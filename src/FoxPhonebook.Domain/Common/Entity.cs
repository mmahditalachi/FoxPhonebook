﻿namespace FoxPhonebook.Domain.Common;
public abstract class Entity<TKey>
{
    public virtual TKey Id { get; protected set; }
}
