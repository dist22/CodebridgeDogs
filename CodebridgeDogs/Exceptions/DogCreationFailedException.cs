namespace CodebridgeDogs.Exceptions;

public class DogCreationFailedException(string massageText) : Exception(massageText);