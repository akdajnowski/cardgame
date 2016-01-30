using System;
using System.Runtime.Serialization;

[Serializable]
internal class InvalidOutcomeTypeException : Exception
{
    public InvalidOutcomeTypeException()
    {
    }

    public InvalidOutcomeTypeException(string message) : base(message)
    {
    }

    public InvalidOutcomeTypeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected InvalidOutcomeTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}