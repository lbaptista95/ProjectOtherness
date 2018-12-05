﻿/*
------------------------------------------------
Generated by Cradle 2.0.1.0
https://github.com/daterre/Cradle

Original file: DyingSoldierDialogue.html
Story format: Harlowe
------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Harlowe;

public partial class @DyingSoldierDialogue: Cradle.StoryFormats.Harlowe.HarloweStory
{
	#region Variables
	// ---------------

	public class VarDefs: RuntimeVars
	{
		public VarDefs()
		{
		}

	}

	public new VarDefs Vars
	{
		get { return (VarDefs) base.Vars; }
	}

	// ---------------
	#endregion

	#region Initialization
	// ---------------

	public readonly Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros macros1;

	@DyingSoldierDialogue()
	{
		this.StartPassage = "Passagem Sem Nome";

		base.Vars = new VarDefs() { Story = this, StrictMode = true };

		macros1 = new Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros() { Story = this };

		base.Init();
		passage1_Init();
		passage2_Init();
		passage3_Init();
		passage4_Init();
		passage5_Init();
	}

	// ---------------
	#endregion

	// .............
	// #1: Passagem Sem Nome

	void passage1_Init()
	{
		this.Passages[@"Passagem Sem Nome"] = new StoryPassage(@"Passagem Sem Nome", new string[]{  }, passage1_Main);
	}

	IStoryThread passage1_Main()
	{
		yield return text("...");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("WHAT HAPPENED?", "WHAT HAPPENED?", null);
		yield break;
	}


	// .............
	// #2: WHAT HAPPENED?

	void passage2_Init()
	{
		this.Passages[@"WHAT HAPPENED?"] = new StoryPassage(@"WHAT HAPPENED?", new string[]{  }, passage2_Main);
	}

	IStoryThread passage2_Main()
	{
		yield return text("They killed... Every one of us");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("WHO?", "WHO?", null);
		yield break;
	}


	// .............
	// #3: WHO?

	void passage3_Init()
	{
		this.Passages[@"WHO?"] = new StoryPassage(@"WHO?", new string[]{  }, passage3_Main);
	}

	IStoryThread passage3_Main()
	{
		yield return text("I don't know... a huge group");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("Where are they?", "Where are they?", null);
		yield break;
	}


	// .............
	// #4: Where are they?

	void passage4_Init()
	{
		this.Passages[@"Where are they?"] = new StoryPassage(@"Where are they?", new string[]{  }, passage4_Main);
	}

	IStoryThread passage4_Main()
	{
		yield return text("...");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("<Exit>", "<Exit>", null);
		yield break;
	}


	// .............
	// #5: <Exit>

	void passage5_Init()
	{
		this.Passages[@"<Exit>"] = new StoryPassage(@"<Exit>", new string[]{  }, passage5_Main);
	}

	IStoryThread passage5_Main()
	{
		yield return text("Clique duas vezes nessa passagem para editá-la.");
		yield break;
	}


}