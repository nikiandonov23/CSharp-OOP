using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Championship.Tests
{
    public class Tests
    {
        [TestFixture]
        public class LeagueTests
        {
            private League league;

            [SetUp]
            public void Setup()
            {
                league = new League();
            }

            [Test]
            public void TestDobaviTeam_Success()
            {
                var team = new Team("ManchesterUnited");

                league.AddTeam(team);

                Assert.AreEqual(1, league.Teams.Count);
                Assert.AreEqual("ManchesterUnited", league.Teams[0].Name);
            }

            [Test]
            public void TestDobaviTeam_LeagueFull()
            {
                for (int i = 0; i < league.Capacity; i++)
                {
                    league.AddTeam(new Team($"Team{i + 1}"));
                }

                Assert.Throws<InvalidOperationException>(() => league.AddTeam(new Team("NewTeam")));
            }

            [Test]
            public void TestDobaviTeam_AlredyExists()
            {
                var team = new Team("ManchesterUnited");
                league.AddTeam(team);

                Assert.Throws<InvalidOperationException>(() => league.AddTeam(new Team("ManchesterUnited")));
            }

            [Test]
            public void TestIztriiTeam_Success()
            {
                var team = new Team("ManchesterUnited");
                league.AddTeam(team);

                bool result = league.RemoveTeam("ManchesterUnited");

                Assert.IsTrue(result);
                Assert.AreEqual(0, league.Teams.Count);
            }

            [Test]
            public void TestRemoveTeam_TeamDoesNotExist()
            {
                bool result = league.RemoveTeam("NonExistingTeam");

                Assert.IsFalse(result);
            }

            [Test]
            public void TestPlayMatch_HomeTeamWins()
            {
                var team1 = new Team("Team1");
                var team2 = new Team("Team2");
                league.AddTeam(team1);
                league.AddTeam(team2);


                // Tesam1 pe4eli
                league.PlayMatch("Team1", "Team2", 3, 1);

                // Proveri Team1 pe4eli li (Uveli4i Wins s 1)
                Assert.AreEqual(1, team1.Wins);  // Team1 should have 1 win

                // Proveri TEam2 gubi (Uveli4i Loses s 1)
                Assert.AreEqual(1, team2.Loses);  // Team 2 trqaa da ima edna zaguba
            }

            [Test]
            public void TestPlayMatch_AwayTeamWins()
            {
                var team1 = new Team("Team1");
                var team2 = new Team("Team2");
                league.AddTeam(team1);
                league.AddTeam(team2);

                league.PlayMatch("Team1", "Team2", 1, 2);

                Assert.AreEqual(1, team1.Loses);
                Assert.AreEqual(1, team2.Wins);
            }

            [Test]
            public void TestPlayMatch_Draw()
            {
                var team1 = new Team("Team1");
                var team2 = new Team("Team2");
                league.AddTeam(team1);
                league.AddTeam(team2);

                league.PlayMatch("Team1", "Team2", 2, 2);

                Assert.AreEqual(1, team1.Draws);
                Assert.AreEqual(1, team2.Draws);
            }

            [Test]
            public void TestPlayMatch_TeamDoesNotExist()
            {
                var team1 = new Team("Team1");
                var team2 = new Team("Team2");
                league.AddTeam(team1);

                Assert.Throws<InvalidOperationException>(() => league.PlayMatch("Team1", "Team3", 1, 1));
            }

            [Test]
            public void TestGetTeamInfo_Success()
            {
                var team = new Team("ManchesterUnited");
                league.AddTeam(team);

                string teamInfo = league.GetTeamInfo("ManchesterUnited");

                Assert.AreEqual("ManchesterUnited - 0 points (0W 0D 0L)", teamInfo);
            }

            [Test]
            public void TestGetTeamInfo_TeamDoesNotExist()
            {
                Assert.Throws<InvalidOperationException>(() => league.GetTeamInfo("NonExistingTeam"));
            }
        }
    }
}