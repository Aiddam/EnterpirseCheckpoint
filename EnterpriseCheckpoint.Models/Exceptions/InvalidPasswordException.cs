namespace EnterpirseCheckpoint.Utilities.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public override string ToString() => "Wrong password!";
    }
}
