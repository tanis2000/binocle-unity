﻿using System;


namespace Binocle.AI.UtilityAI
{
	/// <summary>
	/// scorer for use with a Consideration
	/// </summary>
	public interface IAppraisal<T>
	{
		float getScore( T context );
	}
}

