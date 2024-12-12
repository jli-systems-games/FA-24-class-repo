-> Start

=== Start ===
[The historian stands near the edge of the square, their gaze fixed on the distant forest, a lantern flickering faintly in their hand.]  
[[Historian]]: Ah, a newcomer. Few wanderers find their way to this forgotten place. What brings you here?

* [I’m just passing through.] -> Option_Passing
* [I’ve heard stories about this place.] -> Option_Stories
* [That’s none of your concern.] -> Option_Concern

=== Option_Passing ===
[[You]]: I’m just passing through.  
[[Historian]]: Passing through? No one merely passes through here. This town has a way of pulling people in. Whether you realize it or not, you’re here for a reason.  
-> AskAboutSparks

=== Option_Stories ===
[[You]]: I’ve heard stories about this place.  
[[Historian]]: Stories? Ah, yes, the kind that warn of shadows and curses, no doubt. There’s truth in them, though not always the kind people want to hear.  
-> AskAboutSparks

=== Option_Concern ===
[[You]]: That’s none of your concern.  
[[Historian]]: Perhaps not. But secrets don’t stay hidden here for long. This town has a way of drawing them out.  
-> AskAboutSparks

=== AskAboutSparks ===
[[Historian]]: I assume, if you’ve spoken to anyone in this town, you’ve heard about the sparks?  
* [Yes, I’ve heard a little.] -> Option_Heard
* [No, what are the sparks?] -> Option_WhatSparks

=== Option_Heard ===
[[You]]: Yes, I’ve heard a little.  
[[Historian]]: Then you know they’re more than just flickering lights. Some say they guide the lost. Others say they lead only to ruin.  
[[Historian]]: And yet, they appear without fail, deep in the forest, as if waiting for someone.  

-> AskDecision

=== Option_WhatSparks ===
[[You]]: No, what are the sparks?  
[[Historian]]: Glowing orbs, flickering in the darkest parts of the woods. Some say they’re a guide, others a trap. Whatever they are, they’ve become part of this place’s curse.  

-> AskDecision

=== AskDecision ===
[[Historian]]: So, tell me, what would you do if you saw one?  

* [Follow it, of course.] -> Option_Follow
* [Avoid it. It sounds dangerous.] -> Option_Avoid
* [I’m not sure yet.] -> Option_Unsure

=== Option_Follow ===
[[You]]: Follow it, of course.  
[[Historian]]: Brave, or reckless. Perhaps both. Light may guide, but it also exposes. Be ready for what it reveals.  
-> Lantern_Gift

=== Option_Avoid ===
[[You]]: Avoid it. It sounds dangerous.  
[[Historian]]: Perhaps wise, but remember here, everything eventually finds you. The sparks are no exception.  
-> Lantern_Gift

=== Option_Unsure ===
[[You]]: I’m not sure yet.  
[[Historian]]: Caution suits those who tread these grounds. If the time comes, trust your instincts. They may be your only guide.  
-> Lantern_Gift

=== Lantern_Gift ===
[[Historian]]: Here. If you plan to linger, you’ll need this.  
*[The historian hands you their lantern, its glass etched with strange, swirling patterns. The glow is faint but steady.]*  
[[Historian]]: It’s old, but it’s served me well. May it do the same for you.  

[[Historian]]: Remember, light can guide, but it can also reveal. Be sure you’re ready to see what lies ahead.  
-> End

=== End ===
[The historian nods, their lantern casting flickering shadows as they step away into the growing dusk.]  
-> END