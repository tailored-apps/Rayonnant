using TailoredApps.Rayonnant.Interface.Security;

namespace TailoredApps.Rayonnant.Security
{
    public class Role : IRole
    {
        private Role(string name)
        {
            this.Name = name;
        }
        public string Name { get; private set; }

        public static Role Create(string name)
        {
            return new Role(name);
        }
    }

}
