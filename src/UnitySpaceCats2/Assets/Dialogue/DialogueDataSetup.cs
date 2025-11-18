using Game.Runtime;
using UnityEngine;

/// <summary>
/// Helper script to quickly populate DialoguePathData assets with your dialogue
/// Attach to any GameObject and run the setup methods from Inspector/buttons
/// Or call these methods to programmatically set up dialogue
/// </summary>
public class DialogueDataSetup : MonoBehaviour
{
    [Header("Assign Your 4 Dialogue Path Assets")]
    public DialoguePathData path1Data;
    public DialoguePathData path2Data;
    public DialoguePathData path3Data;
    public DialoguePathData path4Data;
    
    private IGameLogger logger;

    private void Awake()
    {
        logger = ServiceResolver.Resolve<IGameLogger>();
    }
    
    [ContextMenu("Setup All Dialogue")]
    public void SetupAllDialogue()
    {
        SetupPath1Dialogue();
        SetupPath2Dialogue();
        SetupPath3Dialogue();
        SetupPath4Dialogue();
        logger.Log("All dialogue setup complete!");
    }

    [ContextMenu("Setup Path 1 Dialogue")]
    public void SetupPath1Dialogue()
    {
        if (path1Data == null)
        {
            logger.LogError("Path 1 Data not assigned!");
            return;
        }

        // NO STAR DIALOGUE (Path 1 - Option 1)
        path1Data.noStarDialogue = new DialogueNode
        {
            customerLine = "...",
            playerResponseOption1 = "Have a good day.",
            customerFollowUp1 = "",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        // ONE STAR - GOOD DRINK (Path 1 - Option 1)
        path1Data.oneStarGoodDialogue = new DialogueNode
        {
            customerLine = "I guess this is acceptable.",
            playerResponseOption1 = "I'm glad you enjoyed it!",
            customerFollowUp1 = "",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        // ONE STAR - BAD DRINK (Path 1 - Option 1)
        path1Data.oneStarBadDialogue = new DialogueNode
        {
            customerLine = "You seem a bit distracted today… This drink tastes different",
            playerResponseOption1 = "My apologies!",
            customerFollowUp1 = "",
            playerResponseOption2 = "Maybe your tastebuds are a little off today?",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        // THREE STAR - GOOD DRINK (Path 1 - Option 1)
        path1Data.threeStarGoodDialogue = new DialogueNode
        {
            customerLine = "Thanks for the drink! You did great.",
            playerResponseOption1 = "It's always such an honor to hear that from you.",
            customerFollowUp1 = "It's hard to hold back on the compliments when you make such great things. I'll be back for another!",
            playerResponseOption2 = "You're welcome!",
            customerFollowUp2 = "Will you be here tomorrow?",
            hasRemakeOption = false
        };

        // Continue with THREE STAR - BAD, FIVE STAR - GOOD, FIVE STAR - BAD...
        // I'll show the structure, you fill in the rest from your PDF

        path1Data.threeStarBadDialogue = new DialogueNode
        {
            customerLine = "I'm disappointed in you. Your standards are usually higher than this.",
            playerResponseOption1 = "What seems to be the matter?",
            customerFollowUp1 = "This tastes so incredibly wrong it's basically undrinkable.",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = true // This dialogue mentions remake
        };

        path1Data.fiveStarGoodDialogue = new DialogueNode
        {
            customerLine = "You remembered my favorite! You're the best.",
            playerResponseOption1 = "Of course! I couldn't forget one of my regulars!",
            customerFollowUp1 = "Aw thank you. I've been meaning to ask, would you want to grab something after your shift today? There's a new tuna melt place on Jupurrtur if you're interested!",
            playerResponseOption2 = "Maybe next time",
            customerFollowUp2 = "Okay! Another time!",
            hasRemakeOption = false
        };

        path1Data.fiveStarBadDialogue = new DialogueNode
        {
            customerLine = "Woah you look tired! Maybe I should get you a drink!",
            playerResponseOption1 = "Yeah it's been a rough few days.",
            customerFollowUp1 = "Are you sure you're okay working a shift?",
            playerResponseOption2 = "Yeah I spent the last few nights on this new game.",
            customerFollowUp2 = "What game?",
            hasRemakeOption = false
        };

        logger.Log("Path 1 dialogue setup complete!");
    }

    [ContextMenu("Setup Path 2 Dialogue")]
    public void SetupPath2Dialogue()
    {
        if (path2Data == null)
        {
            logger.LogError("Path 2 Data not assigned!");
            return;
        }

        // NO STAR DIALOGUE (Path 2 - Option 2)
        path2Data.noStarDialogue = new DialogueNode
        {
            customerLine = "Thank you.",
            playerResponseOption1 = "You're welcome.",
            customerFollowUp1 = "",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        // ONE STAR - GOOD DRINK (Path 2 - Option 2)
        path2Data.oneStarGoodDialogue = new DialogueNode
        {
            customerLine = "I might have to come here more often!",
            playerResponseOption1 = "Hope to see you around!",
            customerFollowUp1 = "",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        // ONE STAR - BAD DRINK (Path 2 - Option 2)
        path2Data.oneStarBadDialogue = new DialogueNode
        {
            customerLine = "This tastes weird, maybe check your {wrong customizations} stock?",
            playerResponseOption1 = "I'll do better next time…",
            customerFollowUp1 = "",
            playerResponseOption2 = "Well it can't be perfect all the time.",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        // THREE STAR - GOOD DRINK (Path 2 - Option 2)
        path2Data.threeStarGoodDialogue = new DialogueNode
        {
            customerLine = "Somehow I still get surprised at how amazing it tastes every time you make me a drink.",
            playerResponseOption1 = "Really? I feel like I make it like every other barista here.",
            customerFollowUp1 = "No no no, they don't make it with the same love and care as you do.",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        // Fill in the rest from your PDF...
        path2Data.threeStarBadDialogue = new DialogueNode
        {
            customerLine = "Could you tell me how you made this drink? I think you might've gotten a customization wrong.",
            playerResponseOption1 = "I followed what was on your order ticket. Not sure what could've happened.",
            customerFollowUp1 = "Oh, alright it's fine then don't worry about it.",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        path2Data.fiveStarGoodDialogue = new DialogueNode
        {
            customerLine = "This is pretty good. How long have you been working here for?",
            playerResponseOption1 = "Glad to hear you like it! We've only been open a couple of days.",
            customerFollowUp1 = "You've done well for only working a few days. Have you been a barista before?",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        path2Data.fiveStarBadDialogue = new DialogueNode
        {
            customerLine = "You didn't quite make this right. Could you remake it?",
            playerResponseOption1 = "Yes, I apologize, what was wrong with the order?",
            customerFollowUp1 = "The {wrong customization}. I asked for {right customization}",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = true
        };

        logger.Log("Path 2 dialogue setup complete!");
    }

    [ContextMenu("Setup Path 3 Dialogue")]
    public void SetupPath3Dialogue()
    {
        if (path3Data == null)
        {
            logger.LogError("Path 3 Data not assigned!");
            return;
        }

        // NO STAR DIALOGUE (Path 3 - Option 3)
        path3Data.noStarDialogue = new DialogueNode
        {
            customerLine = "This drink is wrong.",
            playerResponseOption1 = "Sorry about that…",
            customerFollowUp1 = "",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        // ONE STAR - GOOD DRINK (Path 3 - Option 3)
        path3Data.oneStarGoodDialogue = new DialogueNode
        {
            customerLine = "Wow! This drink was great!",
            playerResponseOption1 = "Glad you liked it!",
            customerFollowUp1 = "",
            playerResponseOption2 = "Okay.",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        // ONE STAR - BAD DRINK (Path 3 - Option 3)
        path3Data.oneStarBadDialogue = new DialogueNode
        {
            customerLine = "Is everything ok? This isn't your usual standard for drinks.",
            playerResponseOption1 = "Yeah I guess I'm feeling a bit under the weather, I can remake it for you!",
            customerFollowUp1 = "",
            playerResponseOption2 = "Even artists have off days",
            customerFollowUp2 = "",
            hasRemakeOption = true // Option 1 mentions remake
        };

        // THREE STAR - GOOD DRINK (Path 3 - Option 3)
        path3Data.threeStarGoodDialogue = new DialogueNode
        {
            customerLine = "I feel like I'm ascending - this tastes heavenly.",
            playerResponseOption1 = "It's always so nice to see you appreciate my creations.",
            customerFollowUp1 = "I don't know how anyone couldn't appreciate this.",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        path3Data.threeStarBadDialogue = new DialogueNode
        {
            customerLine = "Hi there, it seems like you made my drink incorrectly.",
            playerResponseOption1 = "I'm sorry about that, what seems to be wrong about it?",
            customerFollowUp1 = "You seem to have gotten the {customization} wrong, but it doesn't taste bad. I think it actually improved the recipe!",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        path3Data.fiveStarGoodDialogue = new DialogueNode
        {
            customerLine = "It's nice to see my favorite barista again!",
            playerResponseOption1 = "You flatter me, I'm just doing my job!",
            customerFollowUp1 = "Yeah but no one else makes my drink as perfect as you do! I haven't been able to go to any other coffee shop besides this one.",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        path3Data.fiveStarBadDialogue = new DialogueNode
        {
            customerLine = "Hey, my drink doesn't really taste right… I might be just making things up but it seems to be the {customization}",
            playerResponseOption1 = "Are you sure? I'm positive I made the drink correctly.",
            customerFollowUp1 = "Maybe I'm wrong.. Have a good day.",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        logger.Log("Path 3 dialogue setup complete!");
    }

    [ContextMenu("Setup Path 4 Dialogue")]
    public void SetupPath4Dialogue()
    {
        if (path4Data == null)
        {
            logger.LogError("Path 4 Data not assigned!");
            return;
        }

        // NO STAR DIALOGUE (Path 4 - Option 4)
        path4Data.noStarDialogue = new DialogueNode
        {
            customerLine = "This isn't quite what I was expecting.",
            playerResponseOption1 = "I can remake it for you",
            customerFollowUp1 = "",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = true
        };

        // ONE STAR - GOOD DRINK (Path 4 - Option 4)
        path4Data.oneStarGoodDialogue = new DialogueNode
        {
            customerLine = "Delicious! You made it perfect.",
            playerResponseOption1 = "Nice seeing you again! I'm happy you enjoyed it.",
            customerFollowUp1 = "Have a good day.",
            playerResponseOption2 = "Of course- everything I make is perfect.",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        // ONE STAR - BAD DRINK (Path 4 - Option 4)
        path4Data.oneStarBadDialogue = new DialogueNode
        {
            customerLine = "Can I speak to your manager?",
            playerResponseOption1 = "No sorry… they're busy right now.",
            customerFollowUp1 = "",
            playerResponseOption2 = "No. If you have a problem with your drink you can tell me.",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        // THREE STAR - GOOD DRINK (Path 4 - Option 4)
        path4Data.threeStarGoodDialogue = new DialogueNode
        {
            customerLine = "Ever since this cafe opened I've been spending like half my paycheck on the drinks you make.",
            playerResponseOption1 = "Am I going to have to limit your caffeine from now on?",
            customerFollowUp1 = "NO please! I can't live without your drinks.",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        path4Data.threeStarBadDialogue = new DialogueNode
        {
            customerLine = "...",
            playerResponseOption1 = "Oh no. It tastes bad doesn't it…",
            customerFollowUp1 = "Well, it's definitely not what I was expecting, but it's not terrible! You still did great and I know you're busy so don't worry about it!",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        path4Data.fiveStarGoodDialogue = new DialogueNode
        {
            customerLine = "Delicious. I know I'm having an amazing day when I see you're making my drink!",
            playerResponseOption1 = "Stop it, you always say these things.",
            customerFollowUp1 = "Well it's hard not to when you make me the most scrumptious drinks I've ever had.",
            playerResponseOption2 = "Woah at least buy me dinner before making a move.",
            customerFollowUp2 = "Yeah? How about we head to get purritos down the next planet. It's on me.",
            hasRemakeOption = false
        };

        path4Data.fiveStarBadDialogue = new DialogueNode
        {
            customerLine = "To be honest I'm not a fan of the drink you just made me. It tastes off somehow.",
            playerResponseOption1 = "Sorry to hear that, in what way does it taste off?",
            customerFollowUp1 = "I can't really explain it but it tastes different. Did you change the recipe?",
            playerResponseOption2 = "",
            customerFollowUp2 = "",
            hasRemakeOption = false
        };

        logger.Log("Path 4 dialogue setup complete!");
    }
}