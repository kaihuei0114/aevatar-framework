using Aevatar.Core.Abstractions;

namespace Aevatar.Core.Tests.TestStates;

[GenerateSerializer]
public class GroupAgentState : StateBase
{
    [Id(0)]  public int RegisteredAgents { get; set; } = 0;
}