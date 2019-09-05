    public class ItemNotFound : System.Exception
    {
        public ItemNotFound() : base() { }
        public ItemNotFound(string message) : base(message) { }
        public ItemNotFound(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected ItemNotFound(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }