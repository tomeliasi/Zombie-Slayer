using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    public class CollisionHandler
    {
        private Player player;
        private Form form;
        private List<ZombieAbstract> zombiesList;
        private List<BigZombie> bigZombiesList;



        public CollisionHandler(Player player, Form form, List<ZombieAbstract> zombiesList, List<BigZombie> bigZombiesList)
        {
            this.player = player;
            this.form = form;
            this.zombiesList = zombiesList;
            this.bigZombiesList = bigZombiesList;

        }

        public void HandleCollisions()
        {
            foreach (Control entity in form.Controls)
            {
                switch (entity)
                {
                    case Ammo ammoEntity when player.Bounds.IntersectsWith(ammoEntity.Bounds):
                        HandleAmmoCollision(ammoEntity);
                        break;

                    case HealthKit healthKitEntity when player.Bounds.IntersectsWith(healthKitEntity.Bounds):
                        HandleHealthKitCollision(healthKitEntity);
                        break;

                    case Zombie zombieEntity:
                        HandleZombieCollision(zombieEntity);
                        break;

                    case BigZombie bigZombieEntity:
                        HandleBigZombieCollision(bigZombieEntity);
                        break;

                    case PictureBox bulletEntity when (string)bulletEntity.Tag == "bullet":
                        foreach (Control enemyEntity in form.Controls)
                        {
                            switch (enemyEntity)
                            {
                                case Zombie zombieEnemy when zombieEnemy.Bounds.IntersectsWith(bulletEntity.Bounds):
                                    HandleZombieBulletCollision(zombieEnemy, bulletEntity);
                                    break;

                                case BigZombie bigZombieEnemy when bigZombieEnemy.Bounds.IntersectsWith(bulletEntity.Bounds):
                                    HandleBigZombieBulletCollision(bigZombieEnemy, bulletEntity);
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        private void HandleAmmoCollision(Ammo ammoEntity)
        {
            form.Controls.Remove(ammoEntity);
            ammoEntity.Dispose();

            player.setAmmo(10);
            player.setIsAmmoVisible(false);

            Globals.ammo = null;
            Globals.ammo = new Ammo();
        }

        private void HandleHealthKitCollision(HealthKit healthKitEntity)
        {
            form.Controls.Remove(healthKitEntity);
            healthKitEntity.Dispose();

            if (player.getHealth() <= 70)
                player.setHealth(30);
            else
                player.setMaxHealth();

            player.setIsHealthkitVisable(false);

            Globals.healthKit = null;
            Globals.healthKit = new HealthKit();
        }

        private void HandleZombieCollision(Zombie zombieEntity)
        {
            zombieEntity.move();
            form.Invalidate(zombieEntity.Bounds);

            if (player.Bounds.IntersectsWith(zombieEntity.Bounds))
                player.setHealth(-Constants.ZombieDammage);
        }

        private void HandleBigZombieCollision(BigZombie bigZombieEntity)
        {
            bigZombieEntity.move();
            form.Invalidate(bigZombieEntity.Bounds);

            if (player.Bounds.IntersectsWith(bigZombieEntity.Bounds))
                player.setHealth(-bigZombieEntity.getDemmage());
        }

        private void HandleZombieBulletCollision(Zombie zombieEntity, PictureBox bulletEntity)
        {
            player.setScore(1);
            form.Controls.Remove(bulletEntity);
            bulletEntity.Dispose();
            form.Controls.Remove(zombieEntity);
            zombieEntity.Dispose();
            zombiesList.Remove(zombieEntity);
            Utilities.MakeZombie();
        }

        private void HandleBigZombieBulletCollision(BigZombie bigZombieEntity, PictureBox bulletEntity)
        {
            form.Controls.Remove(bulletEntity);
            bulletEntity.Dispose();

            bigZombieEntity.getDamaged(1);

            if (bigZombieEntity.Width < 100)
            {
                player.setScore(1);
                bigZombiesList.Remove(bigZombieEntity);
                bigZombieEntity.Dispose();
                Utilities.MakeZombie();
            }
        }
    }
}


