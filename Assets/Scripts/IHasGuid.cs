using System;

public interface IHasGuid
{
    Guid id { get; }
}

public interface IHasParentGuid
{
    Guid parentID { get; }
}