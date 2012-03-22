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

			var _projectId = ConfigurationManager.AppSettings["IRON_MQ_PROJECT_ID"];
			var _token = ConfigurationManager.AppSettings["IRON_MQ_TOKEN"];

			_client = new Client(_projectId, _token);
		}

		public void EnqueueAddition(int x, int y)
		{
			_client.queue("operations").push(string.Format("+ {0} {1}", x, y));
		}

		public string DequeueOperation()
		{
			return DequeueMessage("operations");
		}

		public void EnqueueResult(string message)
		{
			_client.queue("results").push(message);
		}

		public string GetLastResult()
		{
			return DequeueMessage("results");
		}

		private string DequeueMessage(string queueName)
		{
			var message = _client.queue(queueName).get();
			if (message != null)
			{
				_client.queue(queueName).deleteMessage(message);
				return message.Body;
			}
			return null;
		}
	}
}
