    public class MechanicNotFound : System.Exception
    {
        public MechanicNotFound() : base() { }
        public MechanicNotFound(string message) : base(message) { }
        public MechanicNotFound(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected MechanicNotFound(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }