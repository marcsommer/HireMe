using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HireMe.DataAccess;
using HireMe.Business.Interfaces;

namespace HireMe.Business
{
  /// <summary>
  /// Abstract generic business base class.  Contains structure for maintaining metastate information.
  /// Fill in your declaring business class as the generic type T, E.g. class Customer : BusinessBase{Customer}
  /// </summary>
  /// <typeparam name="T">Type of declaring business object (e.g. Customer)</typeparam>
  /// <typeparam name="TDto">Type of declaring business object's Dto (e.g. CustomerDto)</typeparam>
  public abstract class BusinessBase<T, TDto> : IBusinessBase<T, TDto>
  {
    #region Ctors and Init

    public BusinessBase()
    {
      _Children = new List<IHaveHeirarchy>();
    }

    #endregion
    
    #region public Guid Id

    /// <summary>
    /// Unique Id for class
    /// </summary>
    public Guid Id
    {
      get { return GetId(); }
      set { SetId(value); }
    }

    protected Guid _Id;
    protected virtual Guid GetId()
    {
      return _Id;
    }
    protected virtual void SetId(Guid value)
    {
      if (value != _Id)
      {
        _Id = value;
        MarkThisDirty();
      }
    }

    #endregion    //Id

    #region public IHaveHeirarchy Parent

    /// <summary>
    /// Parent of this business object, if applicable.
    /// </summary>
    public IHaveHeirarchy Parent
    {
      get { return GetParent(); }
      set { SetParent(value); }
    }

    protected IHaveHeirarchy _Parent;
    public virtual IHaveHeirarchy GetParent()
    {
      return _Parent;
    }
    public virtual void SetParent(IHaveHeirarchy value)
    {
      if (value != _Parent)
      {
        _Parent = value;
        if (!IsLoadingDto)
          MarkThisDirty();
      }
    }

    #endregion    //Parent

    #region public IList<IHaveHeirarchy> Children
    /// <summary>
    /// Child objects that this object is parent of.
    /// </summary>
    public IList<IHaveHeirarchy> Children
    {
      get { return GetChildren(); }
      private set { SetChildren(value); }
    }

    protected IList<IHaveHeirarchy> _Children;
    protected virtual IList<IHaveHeirarchy> GetChildren()
    {
      return _Children;
    }
    protected virtual void SetChildren(IList<IHaveHeirarchy> value)
    {
      if (value != _Children)
      {
        _Children = value;
        if (!IsLoadingDto)
          MarkThisDirty();
      }
    }
    #endregion    //Children

    #region Meta State Properties
    public bool IsDirty
    {
      get
      {
        return GetIsDirty();
      }
    }
    /// <summary>
    /// Checks if this is dirty or if any children are dirty.
    /// </summary>
    /// <returns></returns>
    protected virtual bool GetIsDirty()
    {
      if (ThisIsDirty)
        return true;
      if (HasChildren)
      {
        foreach (var child in Children)
        {
          var dirtyableChild = child as IDirtyable;
          if (dirtyableChild != null && dirtyableChild.IsDirty)
            return true;
        }
      }
      //if we've made it here, then this isn't dirty and no children are dirty. 
      return false;
    }
    public bool ThisIsDirty { get; private set; }
    public bool IsMarkedForDeletion { get; set; }
    public bool IsLoadingDto { get; set; }
    public bool HasChildren
    {
      get { return (Children.Count > 0); }
    }
    public bool UpdateStarted { get; protected set; }
    public bool DeleteStarted { get; protected set; }
    #endregion

    #region Meta State Methods
    /// <summary>
    /// Marks this (not children) object as clean, ie is *NOT* changed/modified, so that when 
    /// Commit/Update is called, this object will *NOT* be sent to DB.
    /// </summary>
    public virtual void MarkThisClean()
    {
      ThisIsDirty = false;
    }
    /// <summary>
    /// Marks the object as dirty, ie this IS changed/modified, so that when 
    /// Commit/Update is called, this object WILL be sent to the DB.
    /// </summary>
    public virtual void MarkThisDirty()
    {
      ThisIsDirty = true;
    }
    /// <summary>
    /// Loads the id directly into the protected backing field, bypassing
    /// marking object as dirty.
    /// </summary>
    /// <param name="id">Id</param>
    protected virtual void SetIdBackingField(Guid id)
    {
      _Id = id;
    }
    #endregion

    #region IHaveDto Methods
    /// <summary>
    /// Wraps LoadFromDtoImpl with try/f Begin/EndDtoLoad.
    /// </summary>
    /// <param name="dto"></param>
    public virtual void LoadFromDto(TDto dto)
    {
      BeginLoadFromDto();
      try
      {
        LoadFromDtoImpl(dto);
      }
      finally
      {
        EndLoadFromDto();
      }
    }
    /// <summary>
    /// Creates Dto from this instance
    /// </summary>
    /// <returns></returns>
    public abstract TDto ToDto();
    /// <summary>
    /// Call this to avoid marking dirty for property loads from DTOs.
    /// </summary>
    protected virtual void BeginLoadFromDto()
    {
      IsLoadingDto = true;
    }
    /// <summary>
    /// Call this after loading from a DTO. This sets IsDirty,IsLoadingDto,
    /// and IsMarkedForDeletion all to false.
    /// </summary>
    protected virtual void EndLoadFromDto()
    {
      IsLoadingDto = false;
      ThisIsDirty = false;
      IsMarkedForDeletion = false;
    }
    /// <summary>
    /// Implement this for loading your object's state from a dto.  It is unnecessary
    /// to wrap this in a Begin/EndDtoLoad block (this is already done for you).
    /// </summary>
    /// <param name="dto">Your business class's Dto</param>
    protected abstract void LoadFromDtoImpl(TDto dto);
    #endregion

    #region Data Access
    /// <summary>
    /// Implement this with code to update your object in DB.
    /// </summary>
    protected abstract void UpdateImpl();
    /// <summary>
    /// Implement this with code to delete your object from the DB.
    /// </summary>
    protected abstract void DeleteImpl();
    /// <summary>
    /// If business object is dirty, then this commits the changes.
    /// Returns the new object that is changed, as the Id may have changed by the 
    /// data providing implemention.
    /// </summary>
    /// <returns></returns>
    public virtual BusinessBase<T, TDto> Commit(bool commitChildren = true)
    {
      if (!IsDirty)
        return this;

      if (IsMarkedForDeletion)
      {
        DeleteImmediately(commitChildren);
        return this;
      }
      else
        return Update(commitChildren);
    }
    /// <summary>
    /// Marks business object for deletion, but does not happen until Commit() is called.
    /// If markChildrenDelete is true, then this also marks ALL children for deletion.
    /// There is no n-level undo, so, you will have to manually unmark any children
    /// that you wish not to delete.
    /// </summary>
    public virtual void Delete(bool deleteChildren = true)
    {
      DeleteStarted = true;
      try
      {
        IsMarkedForDeletion = true;
        MarkThisDirty();
        if (deleteChildren)
          DeleteChildren();
      }
      finally
      {
        DeleteStarted = false;
      }
    }
    /// <summary>
    /// Call this to avoid marking for deletion and then committing.
    /// </summary>
    public virtual void DeleteImmediately(bool deleteChildrenImmediately = true)
    {
      DeleteStarted = true;
      try
      {
        if (deleteChildrenImmediately)
          DeleteChildrenImmediately();

        DeleteImpl();
        MarkThisDirty();
      }
      finally
      {
        DeleteStarted = false;
      }
    }
    /// <summary>
    /// Updates this object.  If updateChildren is true, then calls UpdateChildren()
    /// first.
    /// </summary>
    /// <returns></returns>
    public BusinessBase<T, TDto> Update(bool updateChildren = true)
    {
      UpdateStarted = true;
      try
      {
        if (updateChildren)
          UpdateChildren();
        UpdateImpl();
      }
      finally
      {
        UpdateStarted = false;
      }
      return this;
    }
    #endregion

    #region Child Methods
    /// <summary>
    /// Use this when adding children to make sure meta state is properly maintained.
    /// Adds child to Children, marks this as dirty, sets child.parent to this 
    /// (child can only have one parent).
    /// </summary>
    /// <param name="child">child to add to this object.</param>
    /// <exception cref="ArgumentException">child.Parent must be null</exception>
    public void AddChild(IHaveHeirarchy child)
    {
      if (child.Parent != null)
        throw new ArgumentException();

      Children.Add(child);
      MarkThisDirty();
      child.SetParent(this);
    }
    /// <summary>
    /// Updates all updateable children AND their children recursively.
    /// </summary>
    public virtual void UpdateChildren()
    {
      if (HasChildren)
      {
        List<IHaveHeirarchy> newChildren = new List<IHaveHeirarchy>();
        foreach (var child in Children)
        {
          var updateableChild = child as IUpdateable<T, TDto>;
          if (updateableChild != null && !updateableChild.UpdateStarted) //avoid infinite recursion
            newChildren.Add(updateableChild.Update(true));
        }
        _Children = newChildren;
      }
    }
    /// <summary>
    /// Deletes all children AND their children recursively.
    /// </summary>
    protected virtual void DeleteChildren()
    {
      if (HasChildren)
      {
        foreach (var child in Children)
        {
          var deleteAbleChild = child as IDeleteable;
          if (deleteAbleChild != null && !deleteAbleChild.DeleteStarted) //avoid infinite recursion
            deleteAbleChild.Delete(true);
        }
      }
    }
    /// <summary>
    /// Deletes children immediately AND their children recursively.
    /// </summary>
    protected virtual void DeleteChildrenImmediately()
    {
      if (HasChildren)
      {
        foreach (var child in Children)
        {
          var deleteAbleChild = child as IDeleteable;
          if (deleteAbleChild != null && !deleteAbleChild.DeleteStarted) //avoid infinite recursion
            deleteAbleChild.DeleteImmediately(true);
        }
      }
    }
    #endregion
  }
}
