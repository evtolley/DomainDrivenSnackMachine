using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Persistence.Shared
{
    public class MongoObject<T>
    {
        [BsonId]
        private ObjectId _objectId;
        public T Element { get; }

        public MongoObject(T element)
        {
            _objectId = ObjectId.GenerateNewId();
            Element = element;
        }

        public ObjectId GetObjectId()
        {
            return this._objectId;
        }
    }
}
