using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Reggie
{
    public class AudioManager
    {
        private static AudioManager singletonInstance = null;

        float timer = 0;


        //Songs
        private Song reggie_Ingame_Music;

        //Sounds
        SoundEffect announcer_Denied;
        SoundEffect announcer_Mom_Get_The_Camera;
        SoundEffect announcer_Your_Grandma_Plays_Better;
        SoundEffect announcer110_Get_Rekt;
        SoundEffect announcer110_Victory;
        SoundEffect reggie_Attack_Groaning_1_HUU;
        SoundEffect reggie_Attack_Groaning_2_HAA;
        SoundEffect reggie_Attack_Groaning_3_HEE;
        SoundEffect reggie_Attack_Groaning_4_HOO;
        SoundEffect reggie_Attack_Hits;
        SoundEffect reggie_Equiped_Something;
        SoundEffect reggie_Fell_On_The_Ground;
        SoundEffect reggie_Hurt_1_AU;
        SoundEffect reggie_Hurt_2_AHHH;
        SoundEffect reggie_Jump;
        SoundEffect reggie_Moves;
        SoundEffect reggie_Opens_Schirm;
        SoundEffect reggie_Pickup_Any_Item;
        SoundEffect shopkeeper_Reggie_Bought_Something;
        SoundEffect reggie_Attacks;



        //Konstruktor
        private AudioManager()
        {
            
        }

        //Singleton Pattern
        public static AudioManager AudioManagerInstance()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new AudioManager();
            }
            return singletonInstance;
        }



        public void Play(string s)
        {
            if (s.Equals("Ingame_Music"))
            {
                MediaPlayer.Play(reggie_Ingame_Music);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.1f;
            }
            if (s.Equals("Jump"))
            {
                var temp = reggie_Jump.CreateInstance();
                temp.Volume = 0.1f;
                temp.Play();
            }
            if (s.Equals("Attack") && timer > 0.5f)
            {
                var temp = reggie_Attacks.CreateInstance();
                temp.Volume = 1.0f;
                temp.Play();
                timer = 0;
            }

        }

        //Gets updated via update function in Game1
        public void UpdateAudioManagerTimer(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void LoadSongsAndSound(ContentManager Content)
        {
            loadSongs(Content);
            loadSoundEffects(Content);
        }

        private void loadSongs(ContentManager Content)
        {
            reggie_Ingame_Music = Content.Load<Song>("Audio\\Reggie_Ingame_Music");
        }

        private void loadSoundEffects(ContentManager Content)
        {
            announcer_Denied = Content.Load<SoundEffect>("Audio\\Announcer_Denied");
            announcer_Mom_Get_The_Camera = Content.Load<SoundEffect>("Audio\\Announcer_Mom_Get_The_Camera");
            announcer_Your_Grandma_Plays_Better = Content.Load<SoundEffect>("Audio\\Announcer_Your_Grandma_Plays_Better");
            announcer110_Get_Rekt = Content.Load<SoundEffect>("Audio\\Announcer110_Get_Rekt");
            announcer110_Victory = Content.Load<SoundEffect>("Audio\\Announcer110_Victory");
            reggie_Attack_Groaning_1_HUU = Content.Load<SoundEffect>("Audio\\Reggie_Attack_Groaning_1_HUU");
            reggie_Attack_Groaning_2_HAA = Content.Load<SoundEffect>("Audio\\Reggie_Attack_Groaning_2_HAA");
            reggie_Attack_Groaning_3_HEE = Content.Load<SoundEffect>("Audio\\Reggie_Attack_Groaning_3_HEE");
            reggie_Attack_Groaning_4_HOO = Content.Load<SoundEffect>("Audio\\Reggie_Attack_Groaning_4_HOO");
            reggie_Attack_Hits = Content.Load<SoundEffect>("Audio\\Reggie_Attack_Hits");
            reggie_Equiped_Something = Content.Load<SoundEffect>("Audio\\Reggie_Equiped_Something");
            reggie_Fell_On_The_Ground = Content.Load<SoundEffect>("Audio\\Reggie_Fell_On_The_Ground");
            reggie_Hurt_1_AU = Content.Load<SoundEffect>("Audio\\Reggie_Hurt_1_AU");
            reggie_Hurt_2_AHHH = Content.Load<SoundEffect>("Audio\\Reggie_Hurt_2_AHHH");
            reggie_Jump = Content.Load<SoundEffect>("Audio\\Reggie_Jump");
            reggie_Moves = Content.Load<SoundEffect>("Audio\\Reggie_Moves");
            reggie_Opens_Schirm = Content.Load<SoundEffect>("Audio\\Reggie_Opens_Schirm");
            reggie_Pickup_Any_Item = Content.Load<SoundEffect>("Audio\\Reggie_Pickup_Any_Item");
            shopkeeper_Reggie_Bought_Something = Content.Load<SoundEffect>("Audio\\Shopkeeper_Reggie_Bought_Something");
            reggie_Attacks = Content.Load<SoundEffect>("Audio\\Reggie_Attacks");

            //// Fire and forget play
            //Game1.soundEffectDictionnary["houseChord"].Play();

            //// Play that can be manipulated after the fact
            //var instance = soundEffects[0].CreateInstance();
            //instance.IsLooped = true;
            //instance.Play();
        }

    }
}
