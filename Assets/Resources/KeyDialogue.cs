﻿/*
------------------------------------------------
Generated by Cradle 2.0.1.0
https://github.com/daterre/Cradle

Original file: KeyDialogue.html
Story format: Harlowe
------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Harlowe;

public partial class @KeyDialogue: Cradle.StoryFormats.Harlowe.HarloweStory
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

	@KeyDialogue()
	{
		this.StartPassage = "Passagem Sem Nome";

		base.Vars = new VarDefs() { Story = this, StrictMode = true };

		macros1 = new Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros() { Story = this };

		base.Init();
		passage1_Init();
		passage2_Init();
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
		yield return text("I don't know how long I can take this... After all these years, I just wanted him to know... I just wanted him to know that I forgive him.  ");
		yield return lineBreak();
		yield return text("Please, if you find my brother, give him this letter. We haven't spoken in years, and I don't think I could say those words in person. It hurts too much. ");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("Ok, I'll give it to him", "Ok, I'll give it to him", null);
		yield break;
	}


	// .............
	// #2: Ok, I'll give it to him

	void passage2_Init()
	{
		this.Passages[@"Ok, I'll give it to him"] = new StoryPassage(@"Ok, I'll give it to him", new string[]{  }, passage2_Main);
	}

	IStoryThread passage2_Main()
	{
		yield return text("Clique duas vezes nessa passagem para editá-la.");
		yield break;
	}


}