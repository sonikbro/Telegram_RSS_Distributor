using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_RSS_Distributor
{
    public partial class Form1 : Form
    {
        BackgroundWorker bgWorker;
        String RSSLink = "";
        String choosedCity = "";
        String choosedJobs = "";
        bool chkChooseRetry = false;

        public Form1()
        {
            InitializeComponent();

            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += BgWorker_DoWork;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var txtToken = textBox1.Text;

            if (txtToken != "" && bgWorker.IsBusy != true)
            {
                bgWorker.RunWorkerAsync(txtToken);

                button1.Enabled = false;
                button1.Text = "Connected!";
                button1.ForeColor = Color.Green;
                timer1.Enabled = true;
                timer2.Enabled = true;
                textBox1.Enabled = false;
            }
        }

        async void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            var key = e.Argument as String;

            try
            {
                var bot = new Telegram.Bot.TelegramBotClient(key);
                await bot.SetWebhookAsync("");

                bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
                { 
                    var message = ev.CallbackQuery.Message;

                    switch (ev.CallbackQuery.Data)
                    {
                        case "callback1":
                            {
                                await ChooseJob(sender, e, bot, message);
                                RSSLink = "https://admin10.rabota.ua/Export/Vacancy/feed.ashx?regionId=215";
                                choosedCity = "Belaya Tserkov";
                            }
                            break;
                        case "callback2":
                            {
                                await ChooseJob(sender, e, bot, message);
                                RSSLink = "https://admin10.rabota.ua/Export/Vacancy/feed.ashx?regionId=4";
                                choosedCity = "Dnepr";
                            }
                            break;
                        case "callback3":
                            {
                                await ChooseJob(sender, e, bot, message);
                                RSSLink = "https://admin10.rabota.ua/Export/Vacancy/feed.ashx?regionId=9";
                                choosedCity = "Zaporozhye";
                            }
                            break;
                        case "callback4":
                            {
                                await ChooseJob(sender, e, bot, message);
                                RSSLink = "https://admin10.rabota.ua/Export/Vacancy/feed.ashx?regionId=1";
                                choosedCity = "Kyiv";
                            }
                            break;
                        case "callback5":
                            {
                                await ChooseJob(sender, e, bot, message);
                                RSSLink = "https://admin10.rabota.ua/Export/Vacancy/feed.ashx?regionId=11";
                                choosedCity = "Kropivnitsky";
                            }
                            break;
                        case "callback6":
                            {
                                await ChooseJob(sender, e, bot, message);
                                RSSLink = "https://admin10.rabota.ua/Export/Vacancy/feed.ashx?regionId=3";
                                choosedCity = "Odessa";
                            }
                            break;
                        case "callback7":
                            {
                                await ChooseJob(sender, e, bot, message);
                                RSSLink = "https://admin10.rabota.ua/Export/Vacancy/feed.ashx?regionId=2";
                                choosedCity = "Lviv";
                            }
                            break;
                        case "callback8":
                            {
                                await ChooseJob(sender, e, bot, message);
                                RSSLink = "https://admin10.rabota.ua/Export/Vacancy/feed.ashx?regionId=21";
                                choosedCity = "Kharkiv";
                            }
                            break;
                        case "callback9":
                            {
                                await ChooseJob(sender, e, bot, message);
                                RSSLink = "https://admin10.rabota.ua/Export/Vacancy/feed.ashx?regionId=23";
                                choosedCity = "Khmelnitsky";
                            }
                            break;
                        case "callback10":
                            {
                                await ChooseJob(sender, e, bot, message);
                                RSSLink = "https://admin10.rabota.ua/Export/Vacancy/feed.ashx?regionId=25";
                                choosedCity = "Chernigov";
                            }
                            break;

                    //=========== Choosing the job =============

                        case "callback11":
                            {
                                await AddUser(sender, e, message, RSSLink + "&parentId=1");
                                if (!chkChooseRetry)
                                {
                                    await ChkRetry(sender, e, bot, message);
                                    chkChooseRetry = true;
                                }    
                                await WriteChoosedJobs(sender, e, "IT");
                            }
                            break;
                        case "callback12":
                            {
                                await AddUser(sender, e, message, RSSLink + "&parentId=18");
                                if (!chkChooseRetry)
                                {
                                    await ChkRetry(sender, e, bot, message);
                                    chkChooseRetry = true;
                                }
                                await WriteChoosedJobs(sender, e, "Banks");
                            }
                            break;
                        case "callback13":
                            {
                                await AddUser(sender, e, message, RSSLink + "&parentId=15");
                                if (!chkChooseRetry)
                                {
                                    await ChkRetry(sender, e, bot, message);
                                    chkChooseRetry = true;
                                }     
                                await WriteChoosedJobs(sender, e, "Design");
                            }
                            break;
                        case "callback14":
                            {
                                await AddUser(sender, e, message, RSSLink + "&parentId=24");
                                if (!chkChooseRetry)
                                {
                                    await ChkRetry(sender, e, bot, message);
                                    chkChooseRetry = true;
                                }        
                                await WriteChoosedJobs(sender, e, "Marketing");
                            }
                            break;
                        case "callback15":
                            {
                                await AddUser(sender, e, message, RSSLink + "&parentId=10");
                                if (!chkChooseRetry)
                                {
                                    await ChkRetry(sender, e, bot, message);
                                    chkChooseRetry = true;
                                }             
                                await WriteChoosedJobs(sender, e, "Science");
                            }
                            break;
                        case "callback16":
                            {
                                await AddUser(sender, e, message, RSSLink + "&parentId=26");
                                if (!chkChooseRetry)
                                {
                                    await ChkRetry(sender, e, bot, message);
                                    chkChooseRetry = true;
                                }      
                                await WriteChoosedJobs(sender, e, "Agriculture");
                            }
                            break;
                        case "callback17":
                            {
                                await AddUser(sender, e, message, RSSLink + "&parentId=25");
                                if (!chkChooseRetry)
                                {
                                    await ChkRetry(sender, e, bot, message);
                                    chkChooseRetry = true;
                                }         
                                await WriteChoosedJobs(sender, e, "Marine specialties");
                            }
                            break;
                        case "callback18":
                            {
                                await AddUser(sender, e, message, RSSLink + "&parentId=9");
                                if (!chkChooseRetry)
                                {
                                    await ChkRetry(sender, e, bot, message);
                                    chkChooseRetry = true;
                                }                
                                await WriteChoosedJobs(sender, e, "Medicine");
                            }
                            break;
                        case "callback19":
                            {
                                await AddUser(sender, e, message, RSSLink + "&parentId=30");
                                if (!chkChooseRetry)
                                {
                                    await ChkRetry(sender, e, bot, message);
                                    chkChooseRetry = true;
                                }
                                await WriteChoosedJobs(sender, e, "No experience");
                            }
                            break;

                        // ============= Check Reply ==========
                        case "callback20":
                            {
                                try
                                {
                                    await bot.DeleteMessageAsync(message.Chat.Id, message.MessageId - 2);
                                    await bot.DeleteMessageAsync(message.Chat.Id, message.MessageId - 1);
                                    await bot.EditMessageTextAsync(message.Chat.Id, message.MessageId, "You have successfully subscribed to the newsletter. " + 
                                        "The city You chose: (" + choosedCity + "). Jobs you chose: (" + choosedJobs +
                                        ")." + Environment.NewLine + Environment.NewLine + "Soon there will be news on the specified criteria that You chose.", Telegram.Bot.Types.Enums.ParseMode.Default, false);
                                }
                                catch(Exception ex)
                                {

                                }
                                choosedCity = "";
                                choosedJobs = "";
                                chkChooseRetry = false;
                                // send 3 messages to user (err 429, 'cause too many requests (maybe channel stack overflow))                                
                                //await SendRSStoOneUser(sender, e, bot, message.Chat.Id, RSSLink);                   
                            }
                            break;
                    }
                };

                bot.OnUpdate += async (object su, Telegram.Bot.Args.UpdateEventArgs evu) =>
                {
                    if (evu.Update.CallbackQuery != null || evu.Update.InlineQuery != null) return;
                    var update = evu.Update;
                    var message = update.Message;
                    if (message == null) return;
                    if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                    {
                        if (message.Text == "/start")
                        {
                            var keyboard = new InlineKeyboardMarkup(
                                            new InlineKeyboardButton[][]
                                            {
                                                new []
                                                {
                                                   InlineKeyboardButton.WithCallbackData("Belaya Tserkov", "callback1"),
                                                   InlineKeyboardButton.WithCallbackData("Dnepr", "callback2"),
                                                },
                                                new []
                                                {
                                                   InlineKeyboardButton.WithCallbackData("Zaporozhye", "callback3"),
                                                   InlineKeyboardButton.WithCallbackData("Kyiv", "callback4"),
                                                },
                                                new []
                                                {
                                                   InlineKeyboardButton.WithCallbackData("Kropivnitsky", "callback5"),
                                                   InlineKeyboardButton.WithCallbackData("Odessa", "callback6"),
                                                },
                                                new []
                                                {
                                                   InlineKeyboardButton.WithCallbackData("Lviv", "callback7"),
                                                   InlineKeyboardButton.WithCallbackData("Kharkiv", "callback8"),
                                                },
                                                new []
                                                {
                                                   InlineKeyboardButton.WithCallbackData("Khmelnitsky", "callback9"),
                                                   InlineKeyboardButton.WithCallbackData("Chernigov", "callback10"),
                                                },
                                            }
                                        );
                            await bot.SendTextMessageAsync(message.Chat.Id, "Choose your city:", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyboard);
                        }
                    }
                };
                bot.StartReceiving();
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        async private Task ChooseJob(object sender, EventArgs e, Telegram.Bot.TelegramBotClient bot, Telegram.Bot.Types.Message message)
        {
            await bot.EditMessageTextAsync(message.Chat.Id, message.MessageId, "Great! Now, choose jobs you prefer.", Telegram.Bot.Types.Enums.ParseMode.Default, false);

            var keyboard = new InlineKeyboardMarkup(
                                            new InlineKeyboardButton[][]
                                            {
                                                new []
                                                {
                                                   InlineKeyboardButton.WithCallbackData("IT", "callback11"),
                                                   InlineKeyboardButton.WithCallbackData("Banks", "callback12"),
                                                },
                                                new []
                                                {
                                                   InlineKeyboardButton.WithCallbackData("Design", "callback13"),
                                                   InlineKeyboardButton.WithCallbackData("Marketing", "callback14"),
                                                },
                                                new []
                                                {
                                                   InlineKeyboardButton.WithCallbackData("Science", "callback15"),
                                                   InlineKeyboardButton.WithCallbackData("Agriculture", "callback16"),
                                                },
                                                new []
                                                {
                                                   InlineKeyboardButton.WithCallbackData("Marine specialties", "callback17"),
                                                   InlineKeyboardButton.WithCallbackData("Medicine", "callback18"),
                                                },
                                                new []
                                                {
                                                   InlineKeyboardButton.WithCallbackData("No experience", "callback19"),
                                                },
                                            }
                                        );
          
            await bot.SendTextMessageAsync(message.Chat.Id, "List of jobs: ", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, keyboard);
        }

        async private Task ChkRetry(object sender, EventArgs e, Telegram.Bot.TelegramBotClient bot, Telegram.Bot.Types.Message message)
        {
            var _keyboard = new InlineKeyboardMarkup(
                            new InlineKeyboardButton[][]
                                          {
                                                new []
                                                {
                                                   InlineKeyboardButton.WithCallbackData("I chose the job(s)", "callback20"),
                                                },
                                          }
              );
            await bot.SendTextMessageAsync(message.Chat.Id, "Finished with a choice?", Telegram.Bot.Types.Enums.ParseMode.Default, false, false, 0, _keyboard);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
              XDocument xDoc = XDocument.Load(Application.StartupPath + @"\UsersDB.xml");
               var query = from c in xDoc.Root.Descendants("user")
                           select new
                           {
                               Tgid = c.Element("tgid").Value,
                               RSSLink = c.Element("rsslink").Value,
                           };

               var bot = new Telegram.Bot.TelegramBotClient(textBox1.Text);
               bot.SetWebhookAsync("");

               foreach (var r in query)
               {
                  Task.Run(() => SendRSS(sender, e, bot, Convert.ToInt64(r.Tgid), r.RSSLink));
                  Task.Delay(5000);
               }
               timer1.Enabled = false;
        }

        async private Task SendRSS(Object sender, EventArgs e, Telegram.Bot.TelegramBotClient bot, long userId, string RSSuri)
        {
            XmlReader FeedReader = XmlReader.Create(RSSuri);
           
            SyndicationFeed feed = SyndicationFeed.Load(FeedReader);
            FeedReader.Close();
            
            foreach (SyndicationItem item in feed.Items)
            {
                 String subject = item.Title.Text;
                 String jobLink = item.Links[0].Uri.ToString(); 
                 String summary = item.Summary.Text;
                 await bot.SendTextMessageAsync(userId, jobLink + Environment.NewLine + Environment.NewLine
                       + subject + Environment.NewLine + "\n" + Regex.Replace(StripHTML(summary), ";", "\n"));
                 await Task.Delay(8000);
            }
        }

        public static string StripHTML(string input)
        {
                return Regex.Replace(input, @"<.*?>|\w*&nbsp\w*",  String.Empty);    
        }

        // write to xml
        async private Task AddUser(object sender, EventArgs e, Telegram.Bot.Types.Message message, string RSSUri)
        {
            string xmlFilePath = Application.StartupPath + @"\UsersDB.xml";
            XDocument xDoc = XDocument.Load(xmlFilePath);

            int maxId = xDoc.Root.Elements("user").Max(t => Int32.Parse(t.Attribute("id").Value));
            XElement user = new XElement("user",
                new XAttribute("id", ++maxId),
                new XElement("tgid", message.Chat.Id.ToString()),
                new XElement("rsslink", RSSUri));
            xDoc.Root.Add(user);
            xDoc.Save("UsersDB.xml");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        async private Task WriteChoosedJobs(object sender, EventArgs e, string jobName)
        {
            if (choosedJobs != "")
            {
                choosedJobs += ", " + jobName;
            }
            else
                choosedJobs = jobName;
        }

        async private Task SendRSStoOneUser(Object sender, EventArgs e, Telegram.Bot.TelegramBotClient bot, long userId, string RSSuri)
        {
            await Task.Delay(11000);

            XmlReader FeedReader = XmlReader.Create(RSSuri);

            SyndicationFeed feed = SyndicationFeed.Load(FeedReader);
            FeedReader.Close();
            int stopper = 0;

            foreach (SyndicationItem item in feed.Items)
            {
                if (stopper == 3)
                {
                    return;
                }     
                String subject = item.Title.Text;
                String jobLink = item.Links[0].Uri.ToString();
                String summary = item.Summary.Text;
                await bot.SendTextMessageAsync(userId, jobLink + Environment.NewLine + Environment.NewLine
                      + subject + Environment.NewLine + "\n" + Regex.Replace(StripHTML(summary), ";", "\n"));
                await Task.Delay(8000);

                stopper++;
            }
        }
    }
}