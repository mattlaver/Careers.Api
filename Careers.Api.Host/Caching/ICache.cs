﻿using System;

namespace Careers.Api.Host.Caching
{
    public interface ICache
    {
        void AddOrUpdate(string key, object value, DateTime timeToCache);

        bool TryGet<TItem>(string key, out TItem value) where TItem : class;

        TItem Get<TItem>(string key) where TItem : class;
    }
}