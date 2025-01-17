﻿using SharedKernel;

namespace Persistence.Shared
{
    public static class MongoExtensions
    {
        public static T Unwrap<T>(this MongoObject<T> t) where T : Entity
        {
            if(t == null)
            {
                return null;
            }

            t.Element.Id = t.GetObjectId().ToString();
            return t.Element;
        }
    }
}
