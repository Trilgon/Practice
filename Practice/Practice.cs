//подкючение пространств имён
using System;
using System.Collections.Generic;

namespace University_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Army army = new Army();
            Machine Catapult = new Machine(725, 19, "Catapult", "Bolt", "Electric Phasing Annihilation", 10);
            Soldier Rogue = new Soldier(81, 8, 50, "Steel Armor", "Rogue");
            Soldier Paladin = new Soldier(117, 14, 20, "Leather Armor", "Paladin");
            Soldier Paladin1 = new Soldier(151, 12, 40, "Iron Armor", "Paladin1");
            Soldier Paladin2 = new Soldier(168, 12, 10, "Hide Armor", "Paladin2");
            Animal Rhino = new Animal(74, 30, 27, "Rhino");
            Rider Rider1 = new Rider(Rogue, Rhino, "Rider1");
            Rider Rider2 = new Rider(Paladin, Rhino, "Rider2");
            army.AddToArmy(Catapult);
            army.AddToArmy(Rogue);
            army.AddToArmy(Paladin);
            army.AddToArmy(Paladin1);
            army.AddToArmy(Paladin2);
            army.AddToArmy(Rhino);
            army.AddToArmy(Rider1);
            army.AddToArmy(Rider2);

            army.AboutArmy();

            army.GetDamage(500);
            Console.WriteLine("Бой закончен");

            army.AboutArmy();
        }
    }
}

abstract class Unit
{
    public int Health { get; protected set; }
    public int Armor { get; protected set; }
    public int Damage { get; protected set; }
    public string Type { get; protected set; }
    public virtual void Attack(Unit enemy)
    {
        if (Health > 0)
           enemy.GetDamage(Damage);
    }
    public abstract void About();
    public void GetDamage(int damage)
    {
        damage -= Armor;
        if (damage > 0)
            Health -= damage;
        if (Health < 0)
            Health = 0;
    }
}

class Army
    {
        public int Common_Health { get; private set; }
        public int Common_Damage { get; private set; }
        public int Common_Armor { get; private set; }
        public int Common_Strength { get; private set; }
        private int count = 0;
        private List<Unit> army;
        public Army()
        {
            army = new List<Unit>();
        }
        public void AddToArmy(Unit unit)
        {
            army.Add(unit);
            Common_Health += unit.Health;
            Common_Armor += unit.Armor;
            Common_Damage += unit.Damage;
            Common_Strength++;
        }
        public void GetDamage(int damage)
        {
            while (Common_Strength > 0)
            {
                count++;
                damage -= Common_Armor;
                foreach (var unit in army)
                {                    
                    unit.GetDamage(damage);                 
                }
                for (int i = 0; i < Common_Strength; i++)
                {
                    if (army[i].Health < 1)
                    {
                        army.Remove(army[i]);
                        Common_Strength--;
                        i--;
                    }
                }
                Common_Health = 0;
                Common_Armor = 0;
                Common_Damage = 0;
                foreach (var unit in army)
                {
                    Common_Health += unit.Health;
                    Common_Armor += unit.Armor;
                    Common_Damage += unit.Damage;
                }
                Console.WriteLine($"После {count} атаки:\n" +
                    $"Информация об армии:\n" +
                    $"Кол-во жизней: {Common_Health}\n" +
                    $"Кол-во брони: {Common_Armor}\n" +
                    $"Суммарный урон: {Common_Damage}\n" +
                    $"Численность: {Common_Strength}\n");
            }
        }
        public void AboutArmy()
        {
            if (Common_Strength > 0)
            {
                Console.WriteLine($"Информация об армии:\n" +
                    $"Кол-во жизней: {Common_Health}\n" +
                    $"Кол-во брони: {Common_Armor}\n" +
                    $"Суммарный урон: {Common_Damage}\n" +
                    $"Численность: {Common_Strength}\n");
            }
            else
            {
                Console.WriteLine("В армии нет юнитов");
            }
        }
    }

class Soldier : Unit
{
    private string type_of_Armor;
    public Soldier(int Health, int Damage, int Armor, string type_of_Armor, string Type)//конструктор для записи данных о юните
    {
        this.Type = Type;
        this.Health = Health;
        this.Damage = Damage;
        this.Armor = Armor;
        this.type_of_Armor = type_of_Armor;
    }

    public override void About()
    {
        if (Health > 0)
        {
            Console.WriteLine($"{Type}\n" +
            $"hp = {Health}\n" +
            $"dmg = {Damage}\n" +
            $"Armor = {type_of_Armor} (доп.защита = {Armor})\n");
        }
        else
        {
            Console.WriteLine("Юнит уничтожен\n");
        }
    }
}

class Machine : Unit
{
    private string type_of_ammo;
    private string name_of_weapon;
    private int quantity_of_ammo;
    public Machine(int Health, int Damage, string Type, string type_of_ammo, string name_of_weapon, int quantity_of_ammo)
    {
        this.Type = Type;
        this.Health = Health;
        this.Damage = Damage;
        this.type_of_ammo = type_of_ammo;
        this.name_of_weapon = name_of_weapon;
        this.quantity_of_ammo = quantity_of_ammo;
    }
    public override void About()
    {
        if (Health > 0)
        {
            Console.WriteLine($"{Type}\n" +
            $"ammo = {type_of_ammo} ({name_of_weapon}, {Damage})\n" +
            $"quantity = {quantity_of_ammo}\n" +
            $"hp = {Health}\n");
        }
        else
        {
            Console.WriteLine("Юнит уничтожен\n");
        }
    }

    public override void Attack(Unit enemy)
    {
        if (Health > 0)//проверка состояния
        {
            if (quantity_of_ammo > 0)//проверка количества боеприпасов
            {
                base.Attack(enemy);//вызов базовой реализации метода
            }
            quantity_of_ammo--;//уменьшение боеприпасов на один после атаки
        }
    }
}

class Animal : Unit
{
    private int speed;

    public Animal(int Health, int Damage, int speed, string Type)
    {
        this.Type = Type;
        this.Health = Health;
        this.Damage = Damage;
        this.speed = speed;
    }

    public override void About()
    {
        if (Health > 0)
        {
            Console.WriteLine($"{Type}\n" +
            $"hp = {Health}\n" +
            $"dmg = {Damage}\n" +
            $"spd = {speed}\n");
        }
        else
        {
            Console.WriteLine("Юнит уничтожен\n");
        }
    }
}

class Rider : Unit
{
    Soldier rider;
    Animal mount;

    public Rider(Soldier rider, Animal mount, string Type)
    {
        this.rider = rider;
        this.mount = mount;
        this.Type = Type;
        Health = rider.Health + mount.Health;//подсчёт здоровья всадника
        Damage = rider.Damage + mount.Damage;//подсчёт урон всадника
    }

    public override void About()
    {
        if (Health > 0)
        {
            Console.WriteLine($"Rider\n" +
            $"hp = {Health}\n" +
            $"dmg = {Damage}\n" +
            $"Rider = {rider.Type}\n" +
            $"Mount = {mount.Type}\n");
        }
        else
        {
            Console.WriteLine("Юнит уничтожен\n");
        }
    }
}
