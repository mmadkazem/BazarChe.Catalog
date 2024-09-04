namespace Catalog.Common.Exceptions;

public abstract class CatalogDomainBaseException(string message)
    : Exception(message);