using System;


namespace Binocle.AI.UtilityAI
{
	public interface IAction<T>
	{
		void execute( T context );
	}
}

