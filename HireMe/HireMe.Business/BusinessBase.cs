using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HireMe.DataAccess;

namespace HireMe.Business
{
  /// <summary>
  /// Abstract generic business base class.  Contains structure for maintaining metastate information.
  /// Fill in your declaring business class as the generic type T, E.g. class Customer : BusinessBase{Customer}
  /// </summary>
  /// <typeparam name="T">Type of declaring business object.</typeparam>
  public abstract class BusinessBase<T, TDto>
  {
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
        MarkDirty();
      }
    }

    #endregion    //Id

    #region Meta State Properties

    public bool IsDirty { get; set; }
    public bool IsMarkedForDeletion { get; set; }
    public bool IsLoadingDto { get; set; }

    #endregion

    #region Meta State Methods

    /// <summary>
    /// Marks the object as clean, ie is *NOT* changed/modified, so that when 
    /// Commit/Update is called, this object will *NOT* be sent to DB.
    /// </summary>
    public virtual void MarkClean()
    {
      IsDirty = false;
    }
    /// <summary>
    /// Marks the object as dirty, ie this IS changed/modified, so that when 
    /// Commit/Update is called, this object WILL be sent to the DB.
    /// </summary>
    public virtual void MarkDirty()
    {
      IsDirty = true;
    }
    /// <summary>
    /// Marks business object for deletion, but does not happen until Commit() is called.
    /// </summary>
    public virtual void MarkDelete()
    {
      IsMarkedForDeletion = true;
      IsDirty = true;
    }
    /// <summary>
    /// Call this to avoid marking dirty for property loads from DTOs.
    /// </summary>
    protected virtual void BeginDtoLoad()
    {
      IsLoadingDto = true;
    }
    /// <summary>
    /// Call this after loading from a DTO. This sets IsDirty,IsLoadingDto,
    /// and IsMarkedForDeletion all to false.
    /// </summary>
    protected virtual void EndDtoLoad()
    {
      IsLoadingDto = false;
      IsDirty = false;
      IsMarkedForDeletion = false;
    }
    /// <summary>
    /// Wraps LoadFromDtoImpl with try/f Begin/EndDtoLoad.
    /// </summary>
    /// <param name="dto"></param>
    protected virtual void LoadFromDto(TDto dto)
    {
      BeginDtoLoad();
      try
      {
        LoadFromDtoImpl(dto);
      }
      finally
      {
        EndDtoLoad();
      }
    }
    /// <summary>
    /// Implement this for loading your object's state from a dto.
    /// </summary>
    /// <param name="dto">Your business class's Dto</param>
    protected abstract void LoadFromDtoImpl(TDto dto);
    
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

    #region Data Access

    /// <summary>
    /// Implement this with code to update your object in DB.
    /// </summary>
    protected abstract BusinessBase<T, TDto> UpdateImpl();
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
    public virtual BusinessBase<T, TDto> Commit()
    {
      if (!IsDirty)
        return (this);

      if (IsMarkedForDeletion)
      {
        DeleteImpl();
        return this;
      }
      else
        return UpdateImpl();
    }
    /// <summary>
    /// Call this to avoid marking for deletion and committing.
    /// </summary>
    public virtual void DeleteImmediately()
    {
      DeleteImpl();
    }

    #endregion
  }
}
