﻿using CSharpFunctionalExtensions;

namespace PetHelpers.Domain.Shared;

public class Title : ComparableValueObject
{
    private Title(string text) => Text = text;

    public string Text { get; }

    public static implicit operator string(Title title) => title.Text;

    public static implicit operator Title(string value) => Create(value).Value;

    public static Result<Title, Error> Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length > Constants.MAX_SHORT_TEXT_LENGTH)
            return Errors.General.ValueIsInvalid("Title");

        return new Title(text);
    }

    protected override IEnumerable<IComparable> GetComparableEqualityComponents()
    {
        yield return Text;
    }
}