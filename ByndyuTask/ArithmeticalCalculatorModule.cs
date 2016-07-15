using Ninject.Modules;

namespace ByndyuTask
{
    public class ArithmeticalCalculatorModule : NinjectModule
    {
        public override void Load()
        {

            Bind<ICalculatorEngine>().To<CalculatorEngine>();
            Bind<IExpressionChecker>().To<BracketsChecker>();
            Bind<IExpressionNormalizer>().To<ArithmeticalNormalizer>();
            Bind<Calculator>().ToSelf();
        }
    }
}