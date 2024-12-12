-> Elise_tavern_keeper

== Elise_tavern_keeper ==
[[Tavern Keeper]]: Well now, a traveler. Haven’t seen one of your kind in a long time.
[[You]]: I take it you don’t get many visitors?
[[Tavern Keeper]]: [Chuckles softly.] Not anymore. This town has a way of keeping to itself.
[[Elise, Tavern Keeper]]: The name’s Elise. I keep this place running, what little of it there is.
So, what brings you here?

* [I’m just passing through.] -> elise_passing_through
* [I’m not sure.] -> elise_drawn_here
* [Why does it matter to you?] -> elise_defensive

== elise_passing_through ==
[[You]]: Oh, I’m just passing through.
[[Elise, Tavern Keeper]]: Passing through? That’s what they all say at first. Funny thing, though, most who come through never seem to leave.
Let me give you some advice. This isn’t the kind of place you wander through without paying attention.
-> elise_tavern_continue

== elise_drawn_here ==
[[You]]: I’m not sure. Something drew me here.
[[Elise, Tavern Keeper]]: [Raises an eyebrow.] Hmm. That happens, from time to time. People feeling... pulled to this place.
Whether it’s the town, the forest, or something else, I couldn’t say. But I’ll warn you: curiosity can be costly.
-> elise_tavern_continue

== elise_defensive ==
[[You]]: Why does it matter to you?
[[Elise, Tavern Keeper]]: [Narrows her eyes.] It doesn’t. But around here, asking questions is better than walking blind.
This town has its secrets, and the forest has its... rules.
-> elise_tavern_continue

== elise_tavern_continue ==
[Leaning slightly closer] Let me ask you something. Have you seen any lights? Small, floating ones, just out of reach?

* [Lights? No, should I have?] -> elise_sparks_intro
* [What kind of lights?] -> elise_sparks_warning

== elise_sparks_intro ==
[[You]]: Lights? No, should I have?
[[Elise, Tavern Keeper]]: They call them sparks. Little glowing things in the forest. Some say they guide the lost, but others...
[Pauses] Well, let’s just say the forest decides who it lets out.
-> elise_tavern_exit

== elise_sparks_warning ==
[[You]]: What kind of lights? I don’t know what you’re talking about.
[[Elise, Tavern Keeper]]: [Shrugs.] Good. Maybe you won’t have to.
But if you see them, don’t follow them blindly. The forest has a way of testing people, and not everyone passes.
-> elise_tavern_exit

== elise_tavern_exit ==
[[Elise, Tavern Keeper]]: If you’re set on heading into the forest, take this.
[The Tavern Keeper hands you a tattered map.] It’s still incomplete, but it marks a few key areas in the woods.
You’ll need all the help you can get if you’re planning on walking those paths.
Just don’t say I didn’t warn you.
-> DONE