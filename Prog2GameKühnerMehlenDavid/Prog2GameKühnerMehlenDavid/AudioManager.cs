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
    //Alles für Kopfhörer optimiert, ansonsten viel zu leise!
    public class AudioManager
    {
        private static AudioManager singletonInstance = null;


        //Timer
        float AnnouncerInsult_Timer;
        float reggie_Hurt_Timer;
        float reggie_Attack_Groaning_Timer;

        float announcer_Mom_Get_The_Camera_Timer;
        float announcer110_Victory_Timer;
        float reggie_Attack_Hits_Timer;
        float reggie_Equiped_Something_Timer;
        float reggie_Fell_On_The_Ground_Timer;
        float reggie_Jump_Timer;
        float reggie_Moves_Timer;
        float reggie_Opens_Schirm_Timer;
        float reggie_Pickup_Any_Item_Timer;
        float shopkeeper_Reggie_Bought_Something_Timer;
        float reggie_Attacks_Timer;

        //welches Stöhnen appears
        float randomTimer;
        //für die Zeitabstände zwischen dem Stöhnen
        float kRerollCooldownTimer;
        float jTempCooldown;


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

        //Not a temp variable makes Move Sound Breakable
        SoundEffectInstance reggieMovesSound;

        //Konstruktor
        private AudioManager()
        {
            kRerollCooldownTimer = 0;
            jTempCooldown = 0;
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


        //Plays The Song or Sound you call him with a Keyword
        public void Play(string s)
        {
            if (s.Equals("IngameMusic"))
            {
                MediaPlayer.Play(reggie_Ingame_Music);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 0.1f;
            }
            if (s.Equals("ReggieJump")&& reggie_Jump_Timer>0.1f)
            {
                var temp = reggie_Jump.CreateInstance();
                temp.Volume = 0.03f;
                temp.Play();
                reggie_Jump_Timer = 0;
            }
            if (s.Equals("ReggieAttack") && reggie_Attacks_Timer > 0.5f)
            {
                var temp = reggie_Attacks.CreateInstance();
                temp.Volume = 0.3f;
                temp.Play();
                reggie_Attacks_Timer = 0;
            }
            if (s.Equals("ReggieGroaning"))
            {

                if (kRerollCooldownTimer <= 0)
                {
                    int i = (int)randomTimer % 3;
                    if (i == 0)
                    {
                        jTempCooldown = 1.5f;
                        kRerollCooldownTimer = jTempCooldown;

                    }
                    if (i == 1)
                    {
                        jTempCooldown = 3.0f;
                        kRerollCooldownTimer = jTempCooldown;
                    }

                    if (i == 2)
                    {
                        jTempCooldown = 6.0f;
                        kRerollCooldownTimer = jTempCooldown;
                    }
                }

                //so dass nicht zwei mal die gleichen sounds hintereinander kommen? --> lastSound = blabla; in bedingung --> if (... && lastsound !=...)
                if (reggie_Attack_Groaning_Timer > jTempCooldown)
                {
                    if ((int)randomTimer % 4 == 0)
                    {
                        var temp_0 = reggie_Attack_Groaning_1_HUU.CreateInstance();
                        temp_0.Volume = 0.09f;
                        temp_0.Play();
                        reggie_Attack_Groaning_Timer = 0;
                    }

                    if ((int)randomTimer % 4 == 1)
                    {
                        var temp_1 = reggie_Attack_Groaning_2_HAA.CreateInstance();
                        temp_1.Volume = 0.09f;
                        temp_1.Play();
                        reggie_Attack_Groaning_Timer = 0;
                    }

                    if ((int)randomTimer % 4 == 2)
                    {
                        var temp_2 = reggie_Attack_Groaning_3_HEE.CreateInstance();
                        temp_2.Volume = 0.09f;
                        temp_2.Play();
                        reggie_Attack_Groaning_Timer = 0;
                    }

                    if ((int)randomTimer % 4 == 3)
                    {
                        var temp_3 = reggie_Attack_Groaning_4_HOO.CreateInstance();
                        temp_3.Volume = 0.09f;
                        temp_3.Play();
                        reggie_Attack_Groaning_Timer = 0;
                    }
                }
            }
            //vlt LAUTER!!! --> außerhalb des games mp3 file
            if (s.Equals("ReggieHitsGround")&& reggie_Fell_On_The_Ground_Timer>0.2f)
            {
                var temp = reggie_Fell_On_The_Ground.CreateInstance();
                temp.Volume = 1.0f;
                temp.Play();
                reggie_Fell_On_The_Ground_Timer = 0;
            }
            if (s.Equals("ReggieAttackHits") && reggie_Attack_Hits_Timer > 0.2f)
            {
                var temp = reggie_Attack_Hits.CreateInstance();
                temp.Volume = 0.03f;
                temp.Play();
                reggie_Attack_Hits_Timer = 0;
            }
            if (s.Equals("ReggieEquipedSomething")&&reggie_Equiped_Something_Timer>0.1f) //VOLUME
            {
                var temp = reggie_Equiped_Something.CreateInstance();
                temp.Volume = 0.5f;
                temp.Play();
                reggie_Equiped_Something_Timer = 0;
            }
            if (s.Equals("ReggieOpensSchirm") && reggie_Opens_Schirm_Timer > 0.1f)
            {
                var temp = reggie_Opens_Schirm.CreateInstance();
                temp.Volume = 0.05f;
                temp.Play();
                reggie_Opens_Schirm_Timer = 0;
            }
            if (s.Equals("ReggiePickupAnyItem")&& reggie_Pickup_Any_Item_Timer>0.1f)
            {
                var temp = reggie_Pickup_Any_Item.CreateInstance();
                temp.Volume = 0.003f;
                temp.Play();
                reggie_Pickup_Any_Item_Timer = 0;
            }
            if (s.Equals("ReggieBoughtSomething")&& shopkeeper_Reggie_Bought_Something_Timer>0.1f) //VOLUME
            {
                var temp = shopkeeper_Reggie_Bought_Something.CreateInstance();
                temp.Volume = 0.5f;
                temp.Play();
                shopkeeper_Reggie_Bought_Something_Timer = 0;
            }
            if (s.Equals("ReggieHurt")&& reggie_Hurt_Timer>0.2f)
            {
                if ((int)randomTimer % 2 == 0)
                {
                    var temp = reggie_Hurt_1_AU.CreateInstance();
                    temp.Volume = 0.09f;
                    temp.Play();
                    reggie_Hurt_Timer = 0;
                }
                else
                {
                    var temp = reggie_Hurt_2_AHHH.CreateInstance();
                    temp.Volume = 0.09f;
                    temp.Play();
                    reggie_Hurt_Timer = 0;
                }
            }
            if (s.Equals("ReggieMoves") && reggie_Moves_Timer > 0.2f)
            {
                //PAUSEABLE
                reggieMovesSound.Play();
                reggie_Moves_Timer = 0;
            }

            //so dass nicht zwei mal die gleichen sounds hintereinander kommen? --> lastSound = blabla; in bedingung --> if (... && lastsound !=...)
            if (s.Equals("AnnouncerInsult")&& AnnouncerInsult_Timer>3.0f) //VOLUME ANNOUNCER
            {
                if ((int)randomTimer % 3 == 0)
                {
                    var temp = announcer_Denied.CreateInstance();
                    temp.Volume = 0.5f;
                    temp.Play();
                    AnnouncerInsult_Timer = 0;
                }
                if ((int)randomTimer % 3 == 1)
                {
                    var temp = announcer_Your_Grandma_Plays_Better.CreateInstance();
                    temp.Volume = 0.5f;
                    temp.Play();
                    AnnouncerInsult_Timer = 0;
                }
                if ((int)randomTimer % 3 == 2)
                {
                    var temp = announcer110_Get_Rekt.CreateInstance();
                    temp.Volume = 0.5f;
                    temp.Play();
                    AnnouncerInsult_Timer = 0;
                }
            }
            //STILL NO WINCONDITION
            if (s.Equals("AnnouncerMomGetTheCamera")) //VOLUME
            {
                var temp = announcer_Mom_Get_The_Camera.CreateInstance();
                temp.Volume = 0.5f;
                temp.Play();
                announcer_Mom_Get_The_Camera_Timer = 0;
            }
            //STILL NO WIN CONDITION
            if (s.Equals("AnnouncerVictory")) //VOLUME
            {
                var temp = announcer110_Victory.CreateInstance();
                temp.Volume = 0.5f;
                temp.Play();
                announcer110_Victory_Timer = 0;
            }
        }

        public void Break(string s)
        {
            if (s.Equals("ReggieMoves"))
                reggieMovesSound.Stop();
        }

        public void PrepareSoundEffectInstancesAfterLoadingFilesFunction()
        {
            reggieMovesSound = reggie_Moves.CreateInstance();
            reggieMovesSound.Volume = 0.06f;
        }

        //Gets updated via update function in Game1
        public void UpdateAudioManagerTimer(GameTime gameTime)
        {
            //float nextTimerValue = 0; //--> danach nurnoch wertzuweisungen?

            AnnouncerInsult_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            reggie_Attack_Groaning_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            reggie_Hurt_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            announcer_Mom_Get_The_Camera_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            announcer110_Victory_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            reggie_Attack_Hits_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            reggie_Equiped_Something_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            reggie_Fell_On_The_Ground_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            reggie_Jump_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            reggie_Moves_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            reggie_Opens_Schirm_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            reggie_Pickup_Any_Item_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            shopkeeper_Reggie_Bought_Something_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            reggie_Attacks_Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
       
            randomTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            kRerollCooldownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            //alle timer irgendwann resetten damit nicht ewig groß wird?
        }

        public void LoadSongsAndSound(ContentManager Content)
        {
            loadSongs(Content);
            loadSoundEffects(Content);
            PrepareSoundEffectInstancesAfterLoadingFilesFunction();
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
        }

    }
}
