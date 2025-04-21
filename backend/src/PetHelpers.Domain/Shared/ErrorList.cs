﻿using System.Collections;

namespace PetHelpers.Domain.Shared;

public class ErrorList : IEnumerable<Error>
{
    private readonly List<Error> _errors;

    public ErrorList(IEnumerable<Error> errors)
    {
        _errors = [..errors];
    }

    public static implicit operator ErrorList(List<Error> errors) =>
        new([..errors]);

    public static implicit operator ErrorList(Error error) =>
        new([error]);

    public IEnumerator<Error> GetEnumerator()
    {
        return _errors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}