namespace CodebridgeDogs.Exceptions;

public class DogAlreadyExistsException(string name) : Exception($"Dog with name '{name}' already exists.");