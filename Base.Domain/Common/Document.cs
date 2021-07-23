using MongoDB.Bson;
using System;

namespace Base.Domain.Common
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public string DocumentId => Id.ToString();

        public DateTime CreatedAt => Id.CreationTime;

    }
}
