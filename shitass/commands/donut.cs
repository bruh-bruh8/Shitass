using System.Threading.Tasks;

namespace shitass
{
    class DonutCommand : ICommand
    {
        public string Name => "donut";
        public string[] Aliases => new string[] { };
        public string Description => "donut... yum";
        public string Usage => "donut";

        public Task ExecuteAsync(string[] args, CommandContext context)
        {
            //TOO LAZY
            donutUtil.donut();
            return Task.CompletedTask;
        }
    }
}
