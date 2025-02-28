using UnityEngine;
using System;

public class YieldPreGame : CustomYieldInstruction
{
    Func<bool> condition;
    public override bool keepWaiting => !condition.Invoke();

    public YieldPreGame(Func<bool> condition) => this.condition = condition;
}

public class YieldActiveGame : CustomYieldInstruction
{
    Func<bool> condition;
    public override bool keepWaiting => !condition.Invoke();

    public YieldActiveGame(Func<bool> condition) => this.condition = condition;
}

public class YieldPostGame : CustomYieldInstruction
{
    Func<bool> condition;
    public override bool keepWaiting => !condition.Invoke();

    public YieldPostGame(Func<bool> condition) => this.condition = condition;
}
