namespace Spritesheet
{
	using System.Linq;

	public class AnimationDefinition
	{
		public AnimationDefinition(string name, Frame[] frames)
		{
			this.Name = name;
		}

		public string Name { get; }

	}
}
