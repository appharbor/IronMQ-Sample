using System.Configuration;
using System.Threading.Tasks;
using io.iron.ironmq;

namespace AsynchronousCalculator.Core
{
	public class Calculator
	{
		private readonly Client _client;

		public Calculator()
		{
			TaskScheduler.UnobservedTaskException += (sender, e) =>
			{
				e.SetObserved();
			};

			var _projectId = ConfigurationManager.AppSettings["IRON_MQ_TOKEN"];
			var _token = ConfigurationManager.AppSettings["IRON_MQ_PROJECT_ID"];

			_client = new Client(_projectId, _token);
		}

		public void EnqueueAddition(int x, int y)
		{
			_client.queue("operations").push(string.Format("+ {0} {1}", x, y));
		}

		public string DequeueOperation()
		{
			return _client.queue("operations").get().Body;
		}

		public void EnqueueResult(string message)
		{
			_client.queue("operations").push(message);
		}

		public string GetLastResult()
		{
			return _client.queue("operations").get().Body;
		}
	}
}
