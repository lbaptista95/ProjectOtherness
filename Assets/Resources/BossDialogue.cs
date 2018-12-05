﻿/*
------------------------------------------------
Generated by Cradle 2.0.1.0
https://github.com/daterre/Cradle

Original file: BossDialogue.html
Story format: Harlowe
------------------------------------------------
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;
using IStoryThread = System.Collections.Generic.IEnumerable<Cradle.StoryOutput>;
using Cradle.StoryFormats.Harlowe;

public partial class @BossDialogue: Cradle.StoryFormats.Harlowe.HarloweStory
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

	@BossDialogue()
	{
		this.StartPassage = "(Return)";

		base.Vars = new VarDefs() { Story = this, StrictMode = true };

		macros1 = new Cradle.StoryFormats.Harlowe.HarloweRuntimeMacros() { Story = this };

		base.Init();
		passage1_Init();
		passage2_Init();
		passage3_Init();
		passage4_Init();
		passage5_Init();
		passage6_Init();
	}

	// ---------------
	#endregion

	// .............
	// #1: (Return)

	void passage1_Init()
	{
		this.Passages[@"(Return)"] = new StoryPassage(@"(Return)", new string[]{  }, passage1_Main);
	}

	IStoryThread passage1_Main()
	{
		yield return text("June, once again you disappoint me, you had so much potential as a researcher. But what to expect from women, right? I always said that you only serve for reproduction and should never enter the fleet. ");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("Your sister will never forgive you, you’re trash.", "Your sister will never forgive you, you’re trash.", null);
		yield return text("  ");
		yield return lineBreak();
		yield return link("I do understand your pain, but I have to pass through", "I do understand your pain, but I have to pass through", null);
		yield return text("  ");
		yield return lineBreak();
		yield return link("Get out of my way!", "Get out of my way!", null);
		yield return text(" ");
		yield return lineBreak();
		yield return link("(Hand him the letter)", "(Hand him the letter)", null);
		yield break;
	}


	// .............
	// #2: Your sister will never forgive you, you’re trash.

	void passage2_Init()
	{
		this.Passages[@"Your sister will never forgive you, you’re trash."] = new StoryPassage(@"Your sister will never forgive you, you’re trash.", new string[]{  }, passage2_Main);
	}

	IStoryThread passage2_Main()
	{
		yield return text("What do you know about my sister, you whore? I'll never let you pass, bitch.");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("(Return)", "(Return)", null);
		yield break;
	}


	// .............
	// #3: I do understand your pain, but I have to pass through

	void passage3_Init()
	{
		this.Passages[@"I do understand your pain, but I have to pass through"] = new StoryPassage(@"I do understand your pain, but I have to pass through", new string[]{  }, passage3_Main);
	}

	IStoryThread passage3_Main()
	{
		yield return text("I don’t believe in you!");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("(Return)", "(Return)", null);
		yield break;
	}


	// .............
	// #4: Get out of my way!

	void passage4_Init()
	{
		this.Passages[@"Get out of my way!"] = new StoryPassage(@"Get out of my way!", new string[]{  }, passage4_Main);
	}

	IStoryThread passage4_Main()
	{
		yield return text("Hahahaha who do you think you are? ");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("(Return)", "(Return)", null);
		yield break;
	}


	// .............
	// #5: (Hand him the letter)

	void passage5_Init()
	{
		this.Passages[@"(Hand him the letter)"] = new StoryPassage(@"(Hand him the letter)", new string[]{  }, passage5_Main);
	}

	IStoryThread passage5_Main()
	{
		yield return text("I’ve never imagined that my life would come to that point... Suzanne and I have always been very close to each other. I messed up too. I ended up pushing her far away from my life and, because of that, I lost the way of everything that mattered to me. Thank you, June. I'm going to find her.");
		yield return lineBreak();
		yield return lineBreak();
		yield return link("(Go)", "(Go)", null);
		yield return lineBreak();
		yield break;
	}


	// .............
	// #6: (Go)

	void passage6_Init()
	{
		this.Passages[@"(Go)"] = new StoryPassage(@"(Go)", new string[]{  }, passage6_Main);
	}

	IStoryThread passage6_Main()
	{
		yield return text("Clique duas vezes nessa passagem para editá-la.");
		yield break;
	}


}