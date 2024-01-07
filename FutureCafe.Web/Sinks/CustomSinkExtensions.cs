using Serilog;
using Serilog.Configuration;

namespace FutureCafe.Web.Sinks
{
  public static class CustomSinkExtensions
  {
    public static LoggerConfiguration CustomSink(this LoggerSinkConfiguration loggerConfiguration)
    {
      return loggerConfiguration.Sink(new CustomSink());
    }
  }
}
