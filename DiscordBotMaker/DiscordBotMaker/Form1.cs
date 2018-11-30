using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordBotMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string dbc = desktop + "\\Discord bot - AC\\";
            string path;
            System.IO.Directory.CreateDirectory(dbc);
            System.IO.Directory.CreateDirectory(dbc + "\\commands\\");

            path = dbc + "index.js";
            using (FileStream fs = File.Create(path)) { }
            path = dbc + "package.json";
            using (FileStream fs = File.Create(path)) { }
            path = dbc + "botconfig.json";
            using (FileStream fs = File.Create(path)) { }
            path = dbc + "prefixes.json";
            using (FileStream fs = File.Create(path)) { }

            try {  //index.js
                string text = "const botconfig = require('./botconfig.json');\r\nconst Discord = require('discord.js');\r\nconst fs = require(\"fs\");\r\nconst bot = new Discord.Client({disableEveryone: true});\r\n\r\nbot.commands = new Discord.Collection();\r\n\r\nfs.readdir(\"./commands/\", (err, files) => {\r\n\r\n    if(err) console.log(err);\r\n    let jsfile = files.filter(f => f.split(\".\").pop() === \"js\");\r\n    if(jsfile.length <= 0){\r\n      console.log(\"Couldn't find commands.\");\r\n      return;\r\n    }\r\n  \r\n    jsfile.forEach((f, i) =>{\r\n      let props = require(`./commands/${f}`);\r\n      console.log(`${f} loaded!`);\r\n      bot.commands.set(props.help.name, props);\r\n    });\r\n});\r\n  \r\nbot.on(\"ready\", async () =>  {\r\n      console.log(`${bot.user.username} is online!`);\r\n      bot.user.setActivity('Type +help');\r\n});\r\n\r\nbot.on(\"message\", async message => {\r\n    if(message.author.bot) return;\r\n    if(message.channel.type === \"dm\") return;\r\n\r\n    let prefixes = JSON.parse(fs.readFileSync(\"./prefixes.json\", \"utf8\"));\r\n    if(!prefixes[message.guild.id]){\r\n      prefixes[message.guild.id] = {\r\n        prefixes: botconfig.prefix\r\n      };\r\n    }\r\n    let prefix = prefixes[message.guild.id].prefixes;\r\n    if(!message.content.startsWith(prefix)) return;\r\n\r\n    let messageArray = message.content.split(\" \");\r\n    let cmd = messageArray[0];\r\n    let args = messageArray.slice(1);\r\n\r\n    let commandfile = bot.commands.get(cmd.slice(prefix.length));\r\n    if(commandfile) commandfile.run(bot,message,args);\r\n});\r\n\r\nbot.login(botconfig.token);";
                StreamWriter indexJS = new StreamWriter(dbc + "index.js");

                indexJS.WriteLine(text);
                indexJS.Close();
            } catch {
                Console.WriteLine("Error in writing to index.js");
            }

            try { //package.json
                string text = "{\n" +
                    "   \"name\": \"botname\",\n" +
                    "   \"version\": \"1.0.0\",\n" +
                    "   \"description\": \"Bot Description\",\n" +
                    "   \"main\": \"index.js\",\n" +
                    "   \"scripts\": { \n" +
                    "       \"test\": \"echo \\\"Error: no test specified\\\" && exit 1\"\n" +
                    "   },\n" +
                    "   \"author\": \"6IU\",\n" +
                    "   \"license\": \"ISC\",\n" +
                    "   \"dependencies\": { \n" +
                    "   \"discord.js\": \"^11.4.2\",\n" +
                    "   \"ffmpeg\": \"0.0.4\",\n" +
                    "   \"ms\": \"^2.1.1\",\n" +
                    "   \"opusscript\": \"0.0.6\",\n" +
                    "   \"ytdl-core\": \"^0.28.0\"\n" +
                    "   }\n" +
                    "}";
                StreamWriter packageJSON = new StreamWriter(dbc + "package.json");

                packageJSON.WriteLine(text);
                packageJSON.Close();
            } catch {
                Console.WriteLine("Error in writing to package.json");
            }

            try { //botconfig.json
                string text = "{ \n" +
                    "   \"token\": \"\",\n" +
                    "   \"prefix\": \"+\",\n" +
                    "   \"red\": \"#b70000\",\n" +
                    "   \"orange\": \"#ff6a00\",\n" +
                    "   \"green\": \"#00ff26\",\n" +
                    "   \"purple\": \"#d604cf\",\n" +
                    "   \"yellow\": \"#f4eb42\"\n " +
                    "}";
                StreamWriter botconfigJSON = new StreamWriter(dbc + "botconfig.json");

                botconfigJSON.WriteLine(text);
                botconfigJSON.Close();
            } catch {
                Console.WriteLine("Error in writing to botconfig.json");
            }

            try { //prefixes.json
                string text = "{\n \n}";
                StreamWriter prefixesJSON = new StreamWriter(dbc + "prefixes.json");

                prefixesJSON.WriteLine(text);
                prefixesJSON.Close();
            } catch {
                Console.WriteLine("Error in writing to prefixes.json");
            }

            try
            {
                string strCmdText; strCmdText = "/C cd " + dbc + " && npm i discord.js";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            } catch
            {
                Console.WriteLine("Error installing discord.js");
            }

            try
            {  //commands\commandlayout.js
                string text = "const Discord = require(\"discord.js\");\r\n\r\nmodule.exports.run = async (bot, message, args) => {\r\n\r\n}\r\n\r\nmodule.exports.help = {\r\n    name: \"\"\r\n}";
                StreamWriter indexJS = new StreamWriter(dbc + "\\commands\\" + "commandlayout.js");

                indexJS.WriteLine(text);
                indexJS.Close();
            }
            catch
            {
                Console.WriteLine("Error in writing to comands\\commandlayout.js");
            }
            System.Windows.Forms.Application.ExitThread();
        }
    }
}