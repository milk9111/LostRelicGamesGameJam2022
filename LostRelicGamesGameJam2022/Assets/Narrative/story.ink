
VAR drink = ""

-> charles_start

=== charles_start ===
Hello, I'd like a tall Latte, please 
* [Coming right up!] 
    ~ drink = "latte"
    -> DONE
= convo_start
    How are you today?
    * [Swell, and you?] -> cheerful_response 
    * [<i>(Ignore him)</i>] -> silent_response
= cheerful_response
    That's good to hear. I'm worried it might rain. Do you know what the weather is going to be like? 
    * [Should be clear skies] -> clear_skies
    * [It's definitely going to rain] -> rainy
= silent_response 
    Oh... well sorry to bother you.
    -> DONE
= clear_skies
    Excellent! Then I might just go on a walk with my son today. 
    -> DONE 
= rainy 
    Oh... That's a shame, I was hoping to get out a bit and stretch my legs.
    -> DONE
-> DONE

=== exhausted_dialogue === 
    ... 
    -> DONE
