using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportScheduler
{
	public class ScheduledGame
	{
		public int GameId { get; set; }
		public int Team1 { get; set; }
		public int Team2 { get; set; }
		public int Slot { get; set; }
	}

	public class Schedule
	{
		private readonly Dictionary<int, ScheduledGame> _games = new(); // gameId -> scheduledGame
		private readonly Dictionary<int, List<ScheduledGame>> _slotIndex = new(); // slot -> games
		private readonly Dictionary<int, List<ScheduledGame>> _teamIndex = new(); // teamId -> games

		public void AssignGame(int gameId, int team1, int team2, int slot, int? venue = null)
		{
			var scheduled = new ScheduledGame
			{
				GameId = gameId,
				Team1 = team1,
				Team2 = team2,
				Slot = slot
			};

			_games[gameId] = scheduled;

			if (!_slotIndex.ContainsKey(slot))
				_slotIndex[slot] = new();
			_slotIndex[slot].Add(scheduled);

			foreach (var team in new[] { team1, team2 })
			{
				if (!_teamIndex.ContainsKey(team))
					_teamIndex[team] = new();
				_teamIndex[team].Add(scheduled);
			}
		}

		public ScheduledGame GetGame(int gameId)
			=> _games.TryGetValue(gameId, out var game) ? game : null;

		public List<ScheduledGame> GetGamesByTeam(int teamId)
			=> _teamIndex.TryGetValue(teamId, out var list) ? list : new();

		public List<ScheduledGame> GetGamesBySlot(int slot)
			=> _slotIndex.TryGetValue(slot, out var list) ? list : new();
	}

}
