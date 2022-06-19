
VAR drink = ""

-> charles_start_0.convo_start

=== test === 
This is a test 
* [test] -> test_2
-> DONE

=== test_2 === 
Another test 
* [test] -> test 
-> DONE

=== charles_start_test ===
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
    
=== charles_start_0 === 
Hello, I'd like a tall Hot Latte, please 
* [Coming right up!] 
    ~ drink = "latte"
    -> DONE
= convo_start
    How is your day going? 
    * [Not great, how about yours?] -> not_too_good_either
    * [Not too bad, how about yours?] -> cant_complain
    * [Pretty good, how about yours?] -> going_as_well_as_it_can
= not_too_good_either 
    Not too good either. Headed to the hospital with my wife, Carolyn.
    * [Oh no, hopfully all goes well for you two.] -> hope_for_the_best 
    * [That's unfortunate, I'm sure it'll turn out okay.] -> hope_for_the_best
= cant_complain 
    I can't complain too much. Gonna head to the hospital with my wife soon. 
    * [That's unfortunate, I'm sure it'll turn out okay.] -> hope_for_the_best
    * [How terrible! Is everything alright?] -> hope_for_the_best
    * [That's quite an ordeal this early in the morning.] -> hope_for_the_best
= going_as_well_as_it_can 
    Going about as well as it can be. About to head to the hospital. 
    * [That's quite an ordeal this early in the morning.] -> hope_for_the_best
    * [Glad I'm not going there.] -> silence
= hope_for_the_best 
    We can only hope for the best. It hasn't been going great. 
    * [What's going on? If you don't mind me asking.] -> battling_cancer 
    * [Well I'm sure that she's going to pull through whatever it is] -> pushing_for_four_years 
    * [I'm so sorry to hear that] -> its_alright 
= battling_cancer 
    She's been battling Metastatic Stem Cancer for about four years now.
    * [My aunt was too, fortunately we got it early.] -> glad_aunts_okay 
    * [Oh wow, that's tough.] -> yeah_it_is 
    * [How are you doing through all of it?] -> its_been_hard
= pushing_for_four_years 
    She's been pushing through for four years, I can't say how much more we can take.
    * [Oh wow, that's tough.] -> yeah_it_is 
    * [How are you doing through all of it?] -> its_been_hard
    * [What's wrong, if you don't mind me asking?] -> diagnosed_with_cancer
= its_alright 
    It's alright. 
    * [How are you doing through all of it?] -> its_been_hard
    * [What's wrong, if you don't mind me asking?] -> diagnosed_with_cancer
    * [At least you're not sick] -> silence
= glad_aunts_okay 
    I'm glad to hear that she's doing okay. 
    * [Well it was nice to meet you. Tell your wife I say good luck.] -> hope_your_day_goes_well 
    * [Here's your drink, it was nice meeting you. I'll be keeping your family in my thoughts.] -> hope_your_day_goes_well 
    * [Here's your drink. Have a nice day] -> a_bit_cold 
= yeah_it_is 
    Yes, yes it is 
    * [Well it was nice to meet you. Tell your wife I say good luck.] -> hope_your_day_goes_well 
    * [Here's your drink, it was nice meeting you. I'll be keeping your family in my thoughts.] -> hope_your_day_goes_well 
    * [Here's your drink. Have a nice day] -> a_bit_cold 
= its_been_hard 
    It's been quite hard on me personally. I had to quit my job at Boeing to take care of her. 
    * [Well it was nice to meet you. Tell your wife I say good luck.] -> hope_your_day_goes_well 
    * [Here's your drink, it was nice meeting you. I'll be keeping your family in my thoughts.] -> hope_your_day_goes_well 
    * [Here's your drink. Have a nice day] -> a_bit_cold 
= diagnosed_with_cancer 
    Carolyn was diagnosed with Metastatic Stem Cancer 
    * [Well it was nice to meet you. Tell your wife I say good luck.] -> hope_your_day_goes_well 
    * [Here's your drink, it was nice meeting you. I'll be keeping your family in my thoughts.] -> hope_your_day_goes_well 
    * [Here's your drink. Have a nice day] -> a_bit_cold 
= hope_your_day_goes_well 
    Thank you so much, I hope your day goes well. 
    -> DONE 
= a_bit_cold 
    <i>... He looks at you a bit stunned at your lack of remorse, but doesn't respond...</i>
    -> DONE
= silence 
    <i>silence</i>
    -> DONE
-> DONE
    
=== random_0
Hello, I'd like a small Latte, please 
* [Coming right up!] 
    ~ drink = "latte"
    -> DONE
= convo_start
    How are you today?
    * [Swell, and you?] -> cheerful_response 
    * [<i>(Ignore them)</i>] -> silent_response
= cheerful_response
    That's good to hear. I'm worried it might rain. Do you know what the weather is going to be like? 
    * [Should be clear skies] -> clear_skies
    * [It's definitely going to rain] -> rainy
= silent_response 
    Oh... well sorry to bother you.
    -> DONE
= clear_skies
    Excellent! Then I might just go on a walk with a friend of mine today. 
    -> DONE 
= rainy 
    Oh... That's a shame, I was hoping to get out a bit and stretch my legs.
    -> DONE
    
-> DONE

=== random_1
Can I get a large Mocha? 
* [Absolutely.] 
    ~ drink = "mocha"
    -> DONE
= convo_start
    You having a good shift today?
    * [Yeah, it's been going pretty well so far] -> cheerful_response
    * [As good as it can be] -> neutral_response 
    * [<i>(Ignore them)</i>] -> silent_response
= cheerful_response
    Awesome. Well have a good day!
    -> DONE
= neutral_response 
    Well, I hope it gets better for you.
    -> DONE
= silent_response 
    Ok, sorry I asked.
    -> DONE
-> DONE

=== random_2
Can I get a medium Americano? 
* [Sure, that'll be right up.] 
    ~ drink = "americano"
    -> DONE
= convo_start
    What's new with you?
    * [I've had some great conversations today] -> cheerful_response
    * [Not much, you?] -> neutral_response 
    * [<i>(Ignore them)</i>] -> silent_response
= cheerful_response
    Cool, that's always fun.
    -> DONE
= neutral_response 
    Same old same old.
    -> DONE
= silent_response 
    Well that's just rude...
    -> DONE
-> DONE

=== random_3
Can I get a small Brew? 
* [Sure thing.] 
    ~ drink = "brew"
    -> DONE
= convo_start
    Sorry, I'm not in the mood to chat.
    * [Okay.] -> cheerful_response
= cheerful_response
    ...
    -> DONE
-> DONE

















