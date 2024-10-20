﻿using System.Reflection;
using Application.Abstractions.Messaging;
using Domain.Games;
using Infrastructure.Database;
using Web.Api;

namespace ArchitectureTests;

public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Game).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}
