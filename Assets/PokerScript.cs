using UnityEngine;
using System.Linq;
using System;
using Poker;

    public class PokerScript : MonoBehaviour
    {
        public KMBombInfo Bomb;
        public KMSelectable FoldBut;
        public KMSelectable CheckBut;
        public KMSelectable MinBut;
        public KMSelectable MaxBut;
        public KMSelectable AllInBut;
        public KMSelectable BluffBut;
        public KMSelectable TruthBut;
        public KMSelectable Card1But;
        public KMSelectable Card2But;
        public KMSelectable Card3But;
        public KMSelectable Card4But;
        public KMAudio Audio;
        public Renderer TopCard;
        public Renderer Chip;
        public Texture[]Cards;
        public Texture[]Chips;
        public Texture[]FCard1;
        public Texture[]FCard2;
        public Texture[]FCard3;
        public Texture[]FCard4;
        public TextMesh ResponseText;

        string correctButton;
        string bluffTruth;
        string correctCard;
        Texture card1;
        Texture card2;
        Texture card3;
        Texture card4;
        int stage = 1;
        static int moduleIdCounter = 1;
        int moduleId;
        private string TwitchHelpMessage = "Type '!{0} 'press' followed by either 'fold', 'check', 'min', 'max', 'allin', 'bluff', 'truth', 'card1', 'card2', 'card3' or 'card4' (e.g. !{0} press fold)";

      	public KMSelectable[] ProcessTwitchCommand(string command)
        {
            if (command.Equals("press fold", StringComparison.InvariantCultureIgnoreCase))
            {
                return new KMSelectable[] { FoldBut };
            }
            else if (command.Equals("press check", StringComparison.InvariantCultureIgnoreCase))
            {
                return new KMSelectable[] { CheckBut };
            }
            else if (command.Equals("press min", StringComparison.InvariantCultureIgnoreCase))
            {
                return new KMSelectable[] { MinBut };
            }
            else if (command.Equals("press max", StringComparison.InvariantCultureIgnoreCase))
            {
                return new KMSelectable[] { MaxBut };
            }
            else if (command.Equals("press allin", StringComparison.InvariantCultureIgnoreCase))
            {
                return new KMSelectable[] { AllInBut };
            }
            else if (command.Equals("press bluff", StringComparison.InvariantCultureIgnoreCase))
            {
                return new KMSelectable[] { BluffBut };
            }
            else if (command.Equals("press truth", StringComparison.InvariantCultureIgnoreCase))
            {
                return new KMSelectable[] { TruthBut };
            }
            else if (command.Equals("press card1", StringComparison.InvariantCultureIgnoreCase))
            {
                return new KMSelectable[] { Card1But };
            }
            else if (command.Equals("press card2", StringComparison.InvariantCultureIgnoreCase))
            {
                return new KMSelectable[] { Card2But };
            }
            else if (command.Equals("press card3", StringComparison.InvariantCultureIgnoreCase))
            {
                return new KMSelectable[] { Card3But };
            }
            else if (command.Equals("press card4", StringComparison.InvariantCultureIgnoreCase))
            {
                return new KMSelectable[] { Card4But };
            }
            return null;
        }

        void Awake()
        {
            moduleId = moduleIdCounter++;
        		GetComponent<KMBombModule>().OnActivate += Begin;
            FoldBut.OnInteract += delegate () { ButtonPress("Fold"); return false; };
            CheckBut.OnInteract += delegate () { ButtonPress("Check"); return false; };
            MinBut.OnInteract += delegate () { ButtonPress("MinRaise"); return false; };
            MaxBut.OnInteract += delegate () { ButtonPress("MaxRaise"); return false; };
            AllInBut.OnInteract += delegate () { ButtonPress("AllIn"); return false; };
            BluffBut.OnInteract += delegate () { ButtonPress("Bluff"); return false; };
            TruthBut.OnInteract += delegate () { ButtonPress("Truth"); return false; };
            Card1But.OnInteract += delegate () { ButtonPress("Card1"); return false; };
            Card2But.OnInteract += delegate () { ButtonPress("Card2"); return false; };
            Card3But.OnInteract += delegate () { ButtonPress("Card3"); return false; };
            Card4But.OnInteract += delegate () { ButtonPress("Card4"); return false; };
        }

        void Begin()
        {
            //step 1: pick a card, any card
            int pickedCard = UnityEngine.Random.Range(0, 4); //generate a random number: 0, 1, 2, or 3
            string card = "";

            switch (pickedCard)
            {
                case 0:
                    card = "AceOfSpades";
                    break;
                case 1:
                    card = "KingOfHearts";
                    break;
                case 2:
                    card = "TwoOfClubs";
                    break;
                case 3:
                    card = "FiveOfDiamonds";
                    break;
            }

            TopCard.material.mainTexture = Cards[pickedCard];

            Debug.LogFormat("[Poker #{0}] Your starter card is the {1}.", moduleId, card);

            //step 2: generate a response
            int response = UnityEngine.Random.Range(0, 6); //generate a random number: 0, 1, 2, 3, 4 or 5
            string answer = "";

            switch (response)
            {
                case 0:
                    answer = "Terrible play!";
                    break;
                case 1:
                    answer = "Awful play!";
                    break;
                case 2:
                    answer = "Really?";
                    break;
                case 3:
                    answer = "Really, really?";
                    break;
                case 4:
                    answer = "Sure about that?";
                    break;
                case 5:
                    answer = "Are you sure?";
                    break;
            }

            ResponseText.text = answer;
            ResponseText.color = Color.green;

            Debug.LogFormat("[Poker #{0}] The computer response is {1}.", moduleId, answer);

            //Step 3: generate a chip
            int chipPick = UnityEngine.Random.Range(0, 4); //generate a random chip: 0, 1, 2 or 3
            string chip = "";

            switch (chipPick)
            {
              case 0:
                  chip = "25";
                  break;
              case 1:
                  chip = "50";
                  break;
              case 2:
                  chip = "100";
                  break;
              case 3:
                  chip = "500";
                  break;
            }

            Chip.material.mainTexture = Chips[chipPick];

            Debug.LogFormat("[Poker #{0}] The chip is {1}.", moduleId, chip);

            //Step 4: generate the 4 final cards
            int endCard1 = UnityEngine.Random.Range(0, 4); //generate a random card: 0, 1, 2 or 3
            string FinalCard1 = "";

            switch (endCard1)
            {
              case 0:
                  FinalCard1 = "Club";
                  break;
              case 1:
                  FinalCard1 = "Heart";
                  break;
              case 2:
                  FinalCard1 = "Spade";
                  break;
              case 3:
                  FinalCard1 = "Diamond";
                  break;
            }
            card1 = FCard1[endCard1];



            int endCard2 = UnityEngine.Random.Range(0, 4); //generate a random card: 0, 1, 2 or 3
            string FinalCard2 = "";

            switch (endCard2)
            {
              case 0:
                  FinalCard2 = "Club";
                  break;
              case 1:
                  FinalCard2 = "Heart";
                  break;
              case 2:
                  FinalCard2 = "Spade";
                  break;
              case 3:
                  FinalCard2 = "Diamond";
                  break;
            }

            card2 = FCard2[endCard2];


            int endCard3 = UnityEngine.Random.Range(0, 4); //generate a random card: 0, 1, 2 or 3
            string FinalCard3 = "";

            switch (endCard3)
            {
              case 0:
                  FinalCard3 = "Club";
                  break;
              case 1:
                  FinalCard3 = "Heart";
                  break;
              case 2:
                  FinalCard3 = "Spade";
                  break;
              case 3:
                  FinalCard3 = "Diamond";
                  break;
            }

            card3 = FCard1[endCard3];


            int endCard4 = UnityEngine.Random.Range(0, 4); //generate a random card: 0, 1, 2 or 3
            string FinalCard4 = "";

            switch (endCard4)
            {
              case 0:
                  FinalCard4 = "Club";
                  break;
              case 1:
                  FinalCard4 = "Heart";
                  break;
              case 2:
                  FinalCard4 = "Spade";
                  break;
              case 3:
                  FinalCard4 = "Diamond";
                  break;
            }

            card4 = FCard1[endCard4];


            //step 5: figure out which call is correct
            if (card == "AceOfSpades")
            {
                if (Bomb.GetBatteryCount() >= 3)
                {
                    if (Bomb.IsIndicatorOn("FRK") || Bomb.IsIndicatorOn("BOB"))
                    {
                        if (Bomb.GetSerialNumberNumbers().Sum() % 2 == 0)
                        {
                            if (Bomb.GetPortCount(Port.RJ45) >= 1)
                            {
                                correctButton = "AllIn";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, 3s, 5s, 2s, 4s: a straight flush.", moduleId);
                            }
                            else
                            {
                                correctButton = "MaxRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, 3s, 5s, 2s, 8s: a flush.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.GetPortCount(Port.PS2) >= 1)
                            {
                                correctButton = "MinRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, 3s, 5s, 3d, 3c: three of a kind.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, 3s, 5s, 3d, Ac: two pair.", moduleId);
                            }
                        }
                    }
                    else
                    {
                        if (Bomb.GetBatteryCount(Battery.D) > (Bomb.GetBatteryCount(Battery.AA) + Bomb.GetBatteryCount(Battery.AAx3) + Bomb.GetBatteryCount(Battery.AAx4)))
                        {
                            if (Bomb.GetSerialNumberLetters().Any(x => x == 'A' || x == 'E' || x == 'I' || x == 'O' || x == 'U'))
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, 3s, 9h, Qc, Jd: no hand.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, 3s, 9h, Qc, 3d: a pair.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.GetSerialNumberNumbers().Last() % 2 == 0)
                            {
                                correctButton = "MinRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, 3s, 9h, Ad, Ac: three of a kind.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, 3s, 9h, Ad, 6h: a pair.", moduleId);
                            }
                        }
                    }
                }
                else
                {
                    if (Bomb.GetSerialNumberLetters().Any(x => x == 'A' || x == 'E' || x == 'I' || x == 'O' || x == 'U'))
                    {
                        if (Bomb.IsIndicatorOff("CAR"))
                        {
                            if (Bomb.GetPortCount(Port.DVI) >= 1)
                            {
                                correctButton = "AllIn";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, Jd, Jc, Js, Jh: four of a kind.", moduleId);
                            }
                            else
                            {
                                correctButton = "MaxRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, Jd, Jc, Js, Ah: a full house.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.GetPortCount(Port.Parallel) >= 1)
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, Jd, Jc, Ah, 2C: two pair.", moduleId);
                            }
                            else
                            {
                                correctButton = "MaxRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, Jd, Jc, Ah, Ac: a full house.", moduleId);
                            }
                        }
                    }
                    else
                    {
                        if (Bomb.GetPortCount(Port.Serial) >= 1)
                        {
                            if (Bomb.IsIndicatorOff("SND") || Bomb.IsIndicatorOff("TRN"))
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, Jd, 4c, 6d, 3c: no hand.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, Jd, 4c, 6d, 4s: a pair.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.IsIndicatorOn("SIG") || Bomb.IsIndicatorOn("FRQ"))
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, Jd, 4c, Qd, 10s: no hand.", moduleId);
                            }
                            else //indicator is not present
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is As, Jd, 4c, Qd, Ac: a pair.", moduleId);
                            }
                        }
                    }
                }
            }
            else if (card == "KingOfHearts")
            {
                if (Bomb.GetSerialNumberNumbers().Sum() % 2 == 1)
                {
                    if (Bomb.GetBatteryCount() >= 1)
                    {
                        if (Bomb.IsIndicatorOn("IND") || Bomb.IsIndicatorOn("MSA") || Bomb.IsIndicatorOn("TRN"))
                        {
                            if (Bomb.GetPortCount(Port.StereoRCA) >= 1)
                            {
                                correctButton = "AllIn";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 4c, 4h, 4s, 4d: four of a kind.", moduleId);
                            }
                            else
                            {
                                correctButton = "MaxRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 4c, 4h, 4s, Kc: a full house.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.GetPortCount(Port.RJ45) >= 1 && Bomb.GetPortCount(Port.Serial) >= 1)
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 4c, 4h, Ks, 3c: two pair.", moduleId);
                            }
                            else
                            {
                                correctButton = "MaxRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 4c, 4h, Ks, Kc: a full house.", moduleId);
                            }
                        }
                    }
                    else
                    {
                        if (Bomb.GetPortCount(Port.PS2) >= 1 || Bomb.GetPortCount(Port.DVI) >= 1)
                        {
                            if (Bomb.IsIndicatorOn("SND"))
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 4c, Ah, 2c, 3s: no hand.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 4c, Ah, 2c, 4d: a pair.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.IsIndicatorOff("TRN") || Bomb.IsIndicatorOff("FRK"))
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 4c, Ah, Qs, 3c: no hand.", moduleId);
                            }
                            else
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 4c, Ah, Qs, 5c: no hand.", moduleId);
                            }
                        }
                    }
                }
                else
                {
                    if (Bomb.GetPortCount(Port.Parallel) >= 1)
                    {
                        if (Bomb.GetBatteryCount(Battery.AA) + Bomb.GetBatteryCount(Battery.AAx3) + Bomb.GetBatteryCount(Battery.AAx4) <= 3)
                        {
                            if (Bomb.IsIndicatorOff("MSA") || Bomb.IsIndicatorOn("NSA"))
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 3d, 3s, 2d, 9c: a pair.", moduleId);
                            }
                            else
                            {
                                correctButton = "MinRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 3d, 3s, 2d, 3h: three of a kind.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.IsIndicatorOn("FRQ"))
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 3d, 3s, 7s, 2c: a pair.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 3d, 3s, 7s, 7d: two pair.", moduleId);
                            }
                        }
                    }
                    else
                    {
                        if (Bomb.IsIndicatorOff("BOB") || Bomb.IsIndicatorOff("FRQ"))
                        {
                            //Was a parentheses mismatch here, now corrected
                            if ((Bomb.GetBatteryCount(Battery.AA) + Bomb.GetBatteryCount(Battery.AAx3) + Bomb.GetBatteryCount(Battery.AAx4)) > Bomb.GetBatteryCount(Battery.D))
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 3d, 4h, 10c, Kc: a pair.", moduleId);
                            }
                            else
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 3d, 4h, 10c, Qs: no hand.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.GetBatteryCount() <= 5)
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 3d, 4h, 2h, Ah: no hand.", moduleId);
                            }
                            else //indicator is not present
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is Kh, 3d, 4h, 2h, 9c: no hand.", moduleId);
                            }
                        }
                    }
                }
            }
            else if (card == "FiveOfDiamonds")
            {
                if (Bomb.GetBatteryCount(Battery.D) < (Bomb.GetBatteryCount(Battery.AA) + Bomb.GetBatteryCount(Battery.AAx3) + Bomb.GetBatteryCount(Battery.AAx4)))
                {
                    if (Bomb.GetSerialNumberLetters().Any(x => x == 'A' || x == 'E' || x == 'I' || x == 'O' || x == 'U'))
                    {
                        if (Bomb.GetPortCount() > 1)
                        {
                            if (Bomb.IsIndicatorOff("CLR") || Bomb.IsIndicatorOn("CAR"))
                            {
                                correctButton = "MinRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 3c, 2s, Ah, 4d: a straight.", moduleId);
                            }
                            else
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 3c, 2s, Ah, 6d: no hand.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.IsIndicatorOn("MSA") || Bomb.IsIndicatorOff("NSA"))
                            {
                                correctButton = "MinRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 3c, 2s, 6h, 4s: a straight.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 3c, 2s, 6h, 3d: a pair.", moduleId);
                            }
                        }
                    }
                    else
                    {
                        if (Bomb.GetPortCount(Port.PS2) >= 1 || Bomb.GetPortCount(Port.RJ45) >= 1)
                        {
                            if (Bomb.GetOffIndicators().Count() > 0)
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 3c, 9h, 10s, Jc: no hand.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 3c, 9h, 10s, 9s: a pair.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.IsIndicatorOff("CLR"))
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 3c, 9h, Js, Qh: no hand.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 3c, 9h, Js, 5h: a pair.", moduleId);
                            }
                        }
                    }
                }
                else
                {
                    int lastNumber = Bomb.GetSerialNumberNumbers().Last();
                    if (lastNumber % 2 == 1)
                    {
                        if (Bomb.IsIndicatorOn("BOB") || Bomb.IsIndicatorOff("FRQ") || Bomb.IsIndicatorOff("SIG"))
                        {
                            if (Bomb.GetPortCount() > 0)
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 9d, 4d, Kd, Kh: a pair.", moduleId);
                            }
                            else
                            {
                                correctButton = "MaxRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 9d, 4d, Kd, Qd: a flush.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.GetPortCount(Port.Parallel) >= 1)
                            {
                                correctButton = "MaxRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 9d, 4d, Ad, 2d: a flush.", moduleId);
                            }
                            else
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 9d, 4d, Ad, 6s: no hand.", moduleId);
                            }
                        }
                    }
                    else
                    {
                        if (Bomb.GetOnIndicators().Count() > 0)
                        {
                            if (Bomb.GetPortCount() < 3)
                            {
                                correctButton = "AllIn";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 9d, 6d, 7d, 8d: a straight flush.", moduleId);
                            }
                            else
                            {
                                correctButton = "MinRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 9d, 6d, 7d, 8h: a straight.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.GetPortCount(Port.StereoRCA) >= 1 && Bomb.GetPortCount(Port.DVI) >= 1)
                            {
                                correctButton = "MinRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 9d, 6d, 5c, 5s: three of a kind.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is 5d, 9d, 6d, 5c, 6s: two pair.", moduleId);
                            }
                        }
                    }
                }
            }
            else
            {
                if (Bomb.IsIndicatorOn("TRN") || Bomb.IsIndicatorOn("BOB") || Bomb.IsIndicatorOn("IND"))
                {
                    if (Bomb.GetBatteryCount() <= 5)
                    {
                        if (Bomb.GetPortCount(Port.DVI) >= 1 || Bomb.GetPortCount(Port.StereoRCA) >= 1)
                        {
                            char lastLetter = Bomb.GetSerialNumberLetters().Last();
                            if (lastLetter != 'A' && lastLetter != 'E' && lastLetter != 'I' && lastLetter != 'O' && lastLetter != 'U')
                            {
								                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 6c, 10c, Jh, Qc: no hand.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 6c, 10c, Jh, 2d: a pair.", moduleId);
                            }
                        }
                        else
                        {
                            int lastNumber = Bomb.GetSerialNumberNumbers().Last();
                            if (lastNumber % 2 == 1)
                            {
                                correctButton = "MaxRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 6c, 10c, Jc, 4c: a flush.", moduleId);
                            }
                            else
                            {
                                correctButton = "MaxRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 6c, 10c, Jc, Kc: a flush.", moduleId);
                            }
                        }
                    }
                    else
                    {
                        if (Bomb.GetSerialNumberNumbers().Sum() >= 13)
                        {
                            if (Bomb.GetPortCount(Port.PS2) >= 1 && Bomb.GetPortCount(Port.Parallel) >= 1)
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 6c, As, 2d, 6d: two pair.", moduleId);
                            }
                            else
                            {
                                correctButton = "MinRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 6c, As, 2d, 2h: three of a kind.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.GetPortCount() <= 3)
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 6c, As, 6h, Ah: two pair.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 6c, As, 6h, 7s: a pair.", moduleId);
                            }
                        }
                    }
                }
                else
                {
                    if (Bomb.GetSerialNumberLetters().Count() % 2 == 0)
                    {
                        if (Bomb.GetPortCount(Port.Parallel) >= 1 && Bomb.GetPortCount(Port.Serial) >= 1)
                        {
                            if ((Bomb.GetBatteryCount(Battery.AA) + Bomb.GetBatteryCount(Battery.AAx3) + Bomb.GetBatteryCount(Battery.AAx4)) > Bomb.GetBatteryCount(Battery.D))
                            {
                                correctButton = "MinRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 3h, 4h, 5s, 6c: a straight.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 3h, 4h, 5s, 5d: a pair.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.GetBatteryCount(Battery.D) > (Bomb.GetBatteryCount(Battery.AA) + Bomb.GetBatteryCount(Battery.AAx3) + Bomb.GetBatteryCount(Battery.AAx4)))
                            {
                                correctButton = "MinRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 3h, 4h, 6h, 5h: a straight.", moduleId);
                            }
                            else
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 3h, 4h, 6h, Kh: no hand.", moduleId);
                            }
                        }
                    }
                    else
                    {
                        if (Bomb.GetPortCount(Port.RJ45) >= 1)
                        {
                            if (Bomb.GetBatteryCount(Battery.D) > 2)
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 3h, Kh, Kc, 3s: two pair.", moduleId);
                            }
                            else
                            {
                                correctButton = "MinRaise";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 3h, Kh, Kc, Ks: three of a kind.", moduleId);
                            }
                        }
                        else
                        {
                            if (Bomb.GetBatteryCount() > 2)
                            {
                                correctButton = "Fold";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 3h, Kh, 4d, Jc: no hand.", moduleId);
                            }
                            else
                            {
                                correctButton = "Check";
                                Debug.LogFormat("[Poker #{0}] Your hand is 2c, 3h, Kh, 4d, 2d: a pair.", moduleId);
                            }
                        }
                    }
                }
            }

            //step 6: figure out whether bluff or truth is correct
            if ((card == "AceOfSpades" && (answer == "Terrible play!" || answer == "Really?" || answer == "Sure about that?")) || (card == "KingOfHearts" && (answer == "Terrible play!" || answer == "Awful play!" || answer == "Are you sure?")) || (card == "FiveOfDiamonds" && (answer == "Terrible play!" || answer == "Awful play!" || answer == "Really, really?" || answer == "Are you sure?")) || (card == "TwoOfClubs" && (answer == "Are you sure?" || answer == "Sure about that?")))
            {
                bluffTruth = "Truth";
            }
            else
            {
                bluffTruth = "Bluff";
            }

            //step 7: figure out which of the final cards is correct
            if (chip == "25")
            {
                if ((FinalCard1 == "Diamond" || FinalCard1 == "Heart") && Bomb.IsIndicatorOn("BOB"))
                {
                    correctCard = "Card4";
                }
                else
                {
                    if (answer == "Awful play!" && card == "AceOfSpades")
                    {
                        correctCard = "Card1";
                    }
                    else
                    {
                        if (Bomb.IsIndicatorOff("FRQ") && (FinalCard4 == "Spade" || FinalCard4 == "Club"))
                        {
                            correctCard = "Card2";
                        }
                        else
                        {
                            if ((FinalCard1 == "Diamond" || FinalCard2 == "Diamond" || FinalCard3 == "Diamond" || FinalCard4 == "Diamond") && (answer == "Really?" || answer == "Really, really?"))
                            {
                                correctCard = "Card3";
                            }
                            else
                            {
                                if (FinalCard4 == "Spade" && Bomb.GetBatteryCount() > 4)
                                {
                                    correctCard = "Card3";
                                }
                                else
                                {
                                    if (FinalCard3 == "Diamond" && FinalCard2 != "Club")
                                    {
                                        correctCard = "Card2";
                                    }
                                    else
                                    {
                                        if (answer == "Are you sure?" && card == "TwoOfClubs")
                                        {
                                            correctCard = "Card1";
                                        }
                                        else
                                        {
                                            if (card == "FiveOfDiamonds")
                                            {
                                                correctCard = "Card4";
                                            }
                                            else
                                            {
                                                if (FinalCard2 == "Club" && Bomb.GetPortCount(Port.RJ45) < 1)
                                                {
                                                    correctCard = "Card2";
                                                }
                                                else
                                                {
                                                    correctCard = "Card1";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                if (chip == "50")
                {
                    if (answer == "Sure about that?" && FinalCard4 == "Heart")
                    {
                        correctCard = "Card1";
                    }
                    else
                    {
                        if ((card == "TwoOfClubs") && (FinalCard1 != "Club" && FinalCard2 != "Club" && FinalCard3 != "Club" && FinalCard4 != "Club"))
                        {
                            correctCard = "Card3";
                        }
                        else
                        {
                            if ((FinalCard1 != "Diamond" && FinalCard2 != "Diamond" && FinalCard3 != "Diamond" && FinalCard4 != "Diamond") && ((FinalCard1 == "Heart" && (FinalCard2 == "Spade" || FinalCard3 == "Spade" || FinalCard4 == "Spade")) || (FinalCard2 == "Heart" && (FinalCard3 == "Spade" || FinalCard4 == "Spade")) || (FinalCard3 == "Heart" && FinalCard4 == "Spade")))
                            {
                                correctCard = "Card4";
                            }
                            else
                            {
                                if (FinalCard1 == "Heart" && card != "KingOfHearts")
                                {
                                    correctCard = "Card2";
                                }
                                else
                                {
                                    if (answer == "Really, really?" && (FinalCard1 == "Heart" || FinalCard2 == "Heart"))
                                    {
                                        correctCard = "Card4";
                                    }
                                    else
                                    {
                                        if (card == "FiveOfDiamonds" && Bomb.GetPortCount(Port.Parallel) > 0)
                                        {
                                            correctCard = "Card1";
                                        }
                                        else
                                        {
                                            if (Bomb.IsIndicatorOn("TRN") && ((FinalCard1 == "Club" || FinalCard1 == "Spade") || (FinalCard2 == "Club" || FinalCard2 == "Spade") || (FinalCard3 == "Club" || FinalCard3 == "Spade") || (FinalCard4 == "Club" || FinalCard4 == "Spade")))
                                            {
                                                correctCard = "Card2";
                                            }
                                            else
                                            {
                                                if (answer == "Terrible play!")
                                                {
                                                    correctCard = "Card3";
                                                }
                                                else
                                                {
                                                    if (Bomb.GetSerialNumberNumbers().Sum() < 10)
                                                    {
                                                        correctCard = "Card1";
                                                    }
                                                    else
                                                    {
                                                        correctCard = "Card3";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (chip == "100")
                    {
                        if (answer == "Really, really?")
                        {
                            correctCard = "Card2";
                        }
                        else
                        {
                            if (answer == "Really?")
                            {
                                correctCard = "Card4";
                            }
                            else
                            {
                                if (Bomb.GetBatteryCount(Battery.D) < 1 && card == "AceOfSpades")
                                {
                                    correctCard = "Card1";
                                }
                                else
                                {
                                    if (new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31 }.Contains(Bomb.GetSerialNumberNumbers().Sum()) && (FinalCard1 == "Heart" || FinalCard2 == "Heart" || FinalCard3 == "Heart" || FinalCard4 == "Heart"))
                                    {
                                        correctCard = "Card4";
                                    }
                                    else
                                    {
                                        if ((answer == "Sure about that?") && (FinalCard1 == "Club" || FinalCard2 == "Club" || FinalCard3 == "Club" || FinalCard4 == "Club") && (FinalCard1 == "Spade" || FinalCard2 == "Spade" || FinalCard3 == "Spade" || FinalCard4 == "Spade"))
                                        {
                                            correctCard = "Card3";
                                        }
                                        else
                                        {
                                            if ((FinalCard1 == "Club" & FinalCard2 == "Spade") || (FinalCard1 == "Spade" & FinalCard2 == "Club") || (FinalCard2 == "Club" & FinalCard3 == "Spade") || (FinalCard2 == "Spade" & FinalCard3 == "Club") || (FinalCard3 == "Club" & FinalCard4 == "Spade") || (FinalCard3 == "Spade" & FinalCard4 == "Club"))
                                            {
                                                correctCard = "Card2";
                                            }
                                            else
                                            {
                                                if (Bomb.IsIndicatorOff("MSA"))
                                                {
                                                    correctCard = "Card1";
                                                }
                                                else
                                                {
                                                    if (FinalCard1 == "Diamond" || FinalCard2 == "Diamond" || FinalCard3 == "Diamond" || FinalCard4 == "Diamond")
                                                    {
                                                        correctCard = "Card3";
                                                    }
                                                    else
                                                    {
                                                        if (answer == "Awful play!")
                                                        {
                                                            correctCard = "Card4";
                                                        }
                                                        else
                                                        {
                                                            correctCard = "Card2";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((FinalCard1 == "Club" && FinalCard2 == "Club") || (FinalCard1 == "Club" && FinalCard3 == "Club") || (FinalCard1 == "Club" && FinalCard4 == "Club") || (FinalCard2 == "Club" && FinalCard3 == "Club") || (FinalCard2 == "Club" && FinalCard4 == "Club") || (FinalCard3 == "Club" && FinalCard4 == "Club"))
                        {
                            correctCard = "Card3";
                        }
                        else
                        {
                            if (Bomb.GetSerialNumberLetters().Any(x => x == 'A' || x == 'E' || x == 'I' || x == 'O' || x == 'U') && (FinalCard1 == "Spade" || FinalCard2 == "Spade" || FinalCard3 == "Spade" || FinalCard4 == "Spade"))
                            {
                                correctCard = "Card2";
                            }
                            else
                            {
                                if (Bomb.GetPortCount() < 1 && (FinalCard1 == "Heart" || FinalCard2 == "Heart" || FinalCard3 == "Heart" || FinalCard4 == "Heart"))
                                {
                                    correctCard = "Card1";
                                }
                                else
                                {
                                    if (FinalCard1 != "Heart" && FinalCard1 != "Diamond" && FinalCard2 != "Heart" && FinalCard2 != "Diamond" && FinalCard3 != "Heart" && FinalCard3 != "Diamond" && FinalCard4 != "Heart" && FinalCard4 != "Diamond")
                                    {
                                        correctCard = "Card4";
                                    }
                                    else
                                    {
                                        if (answer == "Are you sure?")
                                        {
                                            correctCard = "Card4";
                                        }
                                        else
                                        {
                                            if (Bomb.GetOnIndicators().Count() < 1 && FinalCard1 == "Heart")
                                            {
                                                correctCard = "Card3";
                                            }
                                            else
                                            {
                                                if (Bomb.GetOffIndicators().Count() > 0 && FinalCard2 == "Club")
                                                {
                                                    correctCard = "Card2";
                                                }
                                                else
                                                {
                                                    if (answer == "Really?" && (FinalCard1 != "Club" && FinalCard1 != "Spade" && FinalCard2 != "Club" && FinalCard2 != "Spade" && FinalCard3 != "Club" && FinalCard3 != "Spade" && FinalCard4 != "Club" && FinalCard4 != "Spade"))
                                                    {
                                                        correctCard = "Card1";
                                                    }
                                                    else
                                                    {
                                                        if (Bomb.GetBatteryCount(Battery.D) > 1)
                                                        {
                                                            correctCard = "Card3";
                                                        }
                                                        else
                                                        {
                                                            correctCard = "Card4";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //Debugger
            Debug.LogFormat("[Poker #{0}] The correct call is {1}.", moduleId, correctButton);
            Debug.LogFormat("[Poker #{0}] Is it a bluff or a truth? It is a {1}.", moduleId, bluffTruth);
            Debug.LogFormat("[Poker #{0}] At the final stage, press {1}.", moduleId, correctCard);
        }

        void ButtonPress(string buttonName)
        {
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            GetComponent<KMSelectable>().AddInteractionPunch();

						switch (stage)
            {
                case 1:
                    if (buttonName == correctButton)
                    {
                        ResponseText.GetComponent<Renderer>().enabled = true;
                        Audio.PlaySoundAtTransform("messageSFX", transform);
                        Debug.LogFormat("[Poker #{0}] You pressed {1}. That is correct.", moduleId, correctButton);
                        stage++;
                    }
                    else
                    {
                        Debug.LogFormat("[Poker #{0}] Strike! You pressed {1}. I was expecting {2}.", moduleId, buttonName, correctButton);
                        GetComponent<KMBombModule>().HandleStrike();
                    }
                    break;

                case 2:
                    if (buttonName == bluffTruth)
                    {
                      Chip.enabled = true;
                      Audio.PlaySoundAtTransform("chipSFX", transform);
                      Card1But.GetComponent<Renderer>().material.mainTexture = card1;
                      Card2But.GetComponent<Renderer>().material.mainTexture = card2;
                      Card3But.GetComponent<Renderer>().material.mainTexture = card3;
                      Card4But.GetComponent<Renderer>().material.mainTexture = card4;
                      Debug.LogFormat("[Poker #{0}] You pressed {1}. That is correct.", moduleId, bluffTruth);
                      stage++;
                    }
                    else
                    {
                        Debug.LogFormat("[Poker #{0}] Strike! You pressed {1}. I was expecting {2}.", moduleId, buttonName, bluffTruth);
                        GetComponent<KMBombModule>().HandleStrike();
                    }
                    break;

                case 3:
                    if (buttonName == correctCard)
                    {
                      Debug.LogFormat("[Poker #{0}] You pressed {1}. That is correct. Module disarmed.", moduleId, correctCard);
                      GetComponent<KMBombModule>().HandlePass();
                    }
                    else
                    {
                        Debug.LogFormat("[Poker #{0}] Strike! You pressed {1}. I was expecting {2}.", moduleId, buttonName, correctCard);
                        GetComponent<KMBombModule>().HandleStrike();
                    }
                    break;

                default:
                        GetComponent<KMBombModule>().HandleStrike();

                    break;
            }
        }
    }
