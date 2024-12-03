-> main

=== main ===
Hi, how are you my dear? You must be far from your home?
    + [Yes very far!]
        -> chosen("Yes")
    + [No actually not so far.]
        -> chosen("No")
    + [Maybe...]
        -> chosen("Maybe")

===chosen(answer)===
{answer}? 
Well anyways my dear, Welcome to the forest!
-> DONE