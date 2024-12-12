-> woodsman_start

== woodsman_start ==
[[Woodsman]]: You’ve got a lot of nerve walking up to me like that.
What do you want? I don’t have time for idle talk.

* [I’m just passing through.] -> woodsman_passing_through
* [What are you doing?] -> woodsman_curiosity
* [I wanted to ask about the forest.] -> woodsman_forest

== woodsman_passing_through ==
You: I’m just passing through the town.
[[Woodsman]]: Then keep moving. This isn’t a place for lingering.
Only two kinds of people come near this forest—those who don’t know better, and those who don’t care to live.
-> woodsman_exit

== woodsman_curiosity ==
[[You]]: I was just curious about what you’re doing.
[[Woodsman]]: Curiosity won’t serve you well in a place like this.
...
Cutting wood. What’s it look like? 
A man’s got to keep busy out here, or the forest might notice him.
-> woodsman_exit

== woodsman_forest ==
[[You]]: I wanted to ask about the forest.
[[Woodsman]]: [Pauses mid-swing, fixing you with a cold glare] Don’t.
The less you know about the forest, the better off you’ll be.
If you’re smart, you’ll stay clear of it and leave the questions for someone else.
-> woodsman_exit

== woodsman_exit ==
Woodsman: That’s enough talk. I’ve got work to do.
[The woodsman lifts his axe and swings hard, the sharp crack of wood splitting echoing through the clearing.]
[Without a glance, he returns to his chopping, the rhythmic thuds carrying on as you walk away.]

-> DONE