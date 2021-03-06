﻿namespace PasswordManager.Models.Data.Queries {

    public interface ISeparatedQueryHandler<TQuery, TResult> where TQuery : ISeparatedQuery<TResult> {

        TResult Execute(TQuery query);
    }
}