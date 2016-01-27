﻿// ***********************************************************************
// Assembly         : Poker
// Class Author     : Alex
//
// Last Modified By : Alex
// Last Modified On : 26 Jan 2016
// ***********************************************************************
// <copyright file="Character.cs" team="Currant">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Drawing;

namespace Poker.Character
{
    using Interfaces;
    using Poker.Interfacees;
    using Poker.Table;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public abstract class Character : ICharacter
    {
        public int chips;
        public string name;
        public bool hasFolded;
        public bool isOnTurn;
        public ICombination cardsCombination;
        public IList<ICard> characterCardsCollection;
        public Point firstCardLocation;
        public Point secondCardLocation;



        private readonly Panel playerPanel = new Panel();
        private readonly Panel firstBotPanel = new Panel();
        private readonly Panel secondBotPanel = new Panel();
        private readonly Panel thirdBotPanel = new Panel();
        private readonly Panel fourthBotPanel = new Panel();
        private readonly Panel fifthBotPanel = new Panel();
        private readonly List<int> playerChips = new List<int>();
        private readonly List<bool?> characterTurn = new List<bool?>();

        public Character(Point firstCardLocation, int cardWidth)
        {
            this.CharacterCardsCollection = new List<ICard>();
            this.FirstCardLocation = firstCardLocation;
            this.SecondCardLocation = GetSecondCardLocation(firstCardLocation, cardWidth);
        }


        public int Chips
        {
            get { return this.chips; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Chips cannot be negative!"); //TODO: CUSTOM Exception: ChipsOutOfRangeException
                }
                if (value > int.MaxValue)
                {
                    this.chips = int.MaxValue;
                }
                this.chips = value;
            }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Character name cannot be null or empty!"); //TODO: CUSTOM Exception:
                }
            }
        }

        public bool HasFolded
        {
            get { return this.hasFolded; }
            set { this.hasFolded = value; }
        }

        public bool IsOnTurn
        {
            get { return this.isOnTurn; }
            set { this.isOnTurn = value; }
        }

        public ICombination CardsCombination
        {
            get { return this.cardsCombination; }
            set { this.cardsCombination = value; }
        }

        public IList<ICard> CharacterCardsCollection
        {
            get { return this.characterCardsCollection; }
            set { this.characterCardsCollection = value; }
        }

        public Point FirstCardLocation
        {
            get { return this.firstCardLocation; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("First Card Location of Character cannot be null");  //TODO: CUSTOM Exception:
                }
                if (value.X < 0 || value.Y < 0)
                {
                    throw new ArgumentOutOfRangeException("First Card Coordinates of Character cannot be negative");    //TODO: CUSTOM Exception:
                }
                this.firstCardLocation = value;
            }
        }

        public Point SecondCardLocation
        {
            get { return this.secondCardLocation; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Second Card Location of Character cannot be null");  //TODO: CUSTOM Exception:
                }
                if (value.X < 0 || value.Y < 0)
                {
                    throw new ArgumentOutOfRangeException("Second Card Coordinates of Character cannot be negative");    //TODO: CUSTOM Exception:
                }
                this.secondCardLocation = value;
            }
        }


        public abstract void Decide(ICharacter character, IList<ICard> cardCollection, int firstCard, int secondCard, int botChips, bool isFinalTurn, Label hasFolded, int botIndex, double botPower, double behaviourPower);

        

        
        /// <summary>
        /// All characters can call an AllIn to play all the money they got
        /// </summary>
        /// <returns></returns>
        public async Task AllIn(TextBox potChips)
        {
            bool isWinning = false;
            Label playerStatus = new Label();
            if (this.Chips <= 0 && !isWinning)
            {
                if (playerStatus.Text.Contains("raise") && playerStatus.Text.Contains("Call"))
                {
                    playerChips.Add(Chips);
                }
            }

            int firstBotChips = 10000;
            bool botOneFirstTurn = false;
            if (firstBotChips <= 0 && !botOneFirstTurn)
            {
                if (!isWinning)
                {
                    playerChips.Add(firstBotChips);
                }
            }

            int secondBotChips = 10000;
            bool botTwoTurn = false;
            if (secondBotChips <= 0 && !botTwoTurn)
            {
                if (!isWinning)
                {
                    playerChips.Add(secondBotChips);
                }
            }

            int thirdBotChips = 10000;
            bool botThreeFirstTurn = false;
            if (thirdBotChips <= 0 && !botThreeFirstTurn)
            {
                if (!isWinning)
                {
                    playerChips.Add(thirdBotChips);
                }
            }

            int fourthBotChips = 10000;
            bool botFourFirstTurn = false;
            if (fourthBotChips <= 0 && !botFourFirstTurn)
            {
                if (!isWinning)
                {
                    playerChips.Add(fourthBotChips);
                }
            }

            int fifthBotChips = 10000;
            bool botFiveFirstTurn = false;
            if (fifthBotChips <= 0 && !botFiveFirstTurn)
            {
                if (!isWinning)
                {
                    playerChips.Add(fifthBotChips);
                }
            }

            int maxLeft = 6;

            if (playerChips.ToArray().Length == maxLeft)
            {
                await Dealer.Finish(2);
            }
            else
            {
                playerChips.Clear();
            }

            var winner = characterTurn.Count(x => x == false);
            if (winner == 1)
            {
                var index = characterTurn.IndexOf(false);
                TextBox tableChips = new TextBox();
                if (index == 0)
                {
                    Chips += int.Parse(potChips.Text);
                    tableChips.Text = this.Chips.ToString();
                    playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    firstBotChips += int.Parse(potChips.Text);
                    tableChips.Text = firstBotChips.ToString();
                    firstBotPanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    secondBotChips += int.Parse(potChips.Text);
                    tableChips.Text = secondBotChips.ToString();
                    secondBotPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    thirdBotChips += int.Parse(potChips.Text);
                    tableChips.Text = thirdBotChips.ToString();
                    thirdBotPanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    fourthBotChips += int.Parse(potChips.Text);
                    tableChips.Text = fourthBotChips.ToString();
                    fourthBotPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    fifthBotChips += int.Parse(potChips.Text);
                    tableChips.Text = fifthBotChips.ToString();
                    fifthBotPanel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }

                for (int j = 0; j <= 16; j++)
                {
                    Dealer.Holder[j].Visible = false;
                }
                await Dealer.Finish(1);
            }

            double rounds = 0;
            int End = 4;
            if (winner < 6 && winner > 1 && rounds >= End)
            {
                await Dealer.Finish(2);
            }
        }

        private bool isRaising;
        /// <summary>
        /// Fold tell us who is giving up
        /// </summary>
        /// <param name="isOnTurn">if set to <c>true</c> [is on turn].</param>
        /// <param name="isFinalTurn">if set to <c>true</c> [is final turn].</param>
        /// <param name="hasFolded">The has folded.</param>
        public void Fold(ref bool isOnTurn, ref bool isFinalTurn, Label hasFolded)
        {
            isRaising = false;
            hasFolded.Text = "Fold";
            isOnTurn = false;
            isFinalTurn = true;
        }

        /// <summary>
        /// Changes the label status to checking.
        /// </summary>
        /// <param name="isBotsTurn">if set to <c>true</c> [is bots turn].</param>
        /// <param name="statusLabel">The status label.</param>
        public void ChangeStatusToChecking(ref bool isBotsTurn, Label statusLabel)
        {
            statusLabel.Text = "Check";
            isBotsTurn = false;
            isRaising = false;
        }

        private int call = 500;
        /// <summary>
        /// You call the required amount of chips to continue playing the game
        /// </summary>
        /// <param name="botChips">The bot chips.</param>
        /// <param name="isBotsTurn">if set to <c>true</c> [is bots turn].</param>
        /// <param name="statusLabel">The status label.</param>
        public void Call(ref int botChips, ref bool isBotsTurn, Label statusLabel,TextBox potChips)
        {
            isRaising = false;
            isBotsTurn = false;
            botChips -= call;
            statusLabel.Text = "Call " + call;
            potChips.Text = (int.Parse(potChips.Text) + call).ToString();
        }

        private double raise = 0;
        /// <summary>
        /// Raises the bet.
        /// </summary>
        /// <param name="botChips">The bot chips.</param>
        /// <param name="isBotsTurn">if set to <c>true</c> [is bots turn].</param>
        /// <param name="statusLabel">The status label.</param>
        public void RaiseBet(ref int botChips, ref bool isBotsTurn, Label statusLabel,TextBox potChips)
        {
            botChips -= Convert.ToInt32(raise);
            statusLabel.Text = "Raise " + raise;
            potChips.Text = (int.Parse(potChips.Text) + Convert.ToInt32(raise)).ToString();
            call = Convert.ToInt32(raise);
            isRaising = true;
            isBotsTurn = false;
        }

        public Point GetSecondCardLocation(Point firstCardLocation, int cardWidth)
        {
            Point secondCardLocation = new Point();

            secondCardLocation.Y = firstCardLocation.Y;
            secondCardLocation.X = (firstCardLocation.X + cardWidth);

            return secondCardLocation;
        }
    }
}