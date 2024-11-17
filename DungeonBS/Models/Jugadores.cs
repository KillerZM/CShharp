using System;
using System.Collections.Generic;
using System.Numerics;
using DungeonBS.Abilities;


namespace DungeonBS.Models { 
    public class Jugadores
{
    // Atributos básicos de jugador
    public string Nick { get; private set; }
    public int Vidas { get; private set; }
    public int Lvl { get; private set; }
    public int Exp { get; private set; }
    public int ExpNextLvl { get; private set; }
    public int Salud { get; private set; }
    public int MaxSalud{get; private set;}
    public bool Estado { get; private set; }
    public int Mana { get; private set; }
    public int MaxMana { get; private set; }
    public int Damage {get; private set; }
    public int MagicDamage{get; private set; }
    public int Armor { get; private set; }
    public int MagicResistance { get; private set; }
    
    // Atributos dinámicos y editables por el jugador, inventario y equipamiento.
    public List<Items> Inventario { get; private set; }
    public List<Effects> Efectos { get; private set; }
    public List<Skills> Habilidades { get; private set; }
    public Dictionary<string, Items> Equipamiento { get; private set; }

    public Jugadores(string nombre)
    {
        //Atributos basicos de identidad
        Nick = nombre;
        Vidas = 2;
        MaxSalud = 100;
        Salud= MaxSalud;
        Lvl = 0;
        Exp = 0;
        ExpNextLvl = 100;
        Estado = true;
        //Atributos de Combate
        Damage = 10;
        MagicDamage = 5;
        MaxMana = 100;
        Mana = MaxMana;
        Armor = 10;
        MagicResistance= 5;
        //Atributos Dinamicos Inventario, habilidades ,equipamiento y efectos
        Inventario = new List<Items>();
        Equipamiento = new Dictionary<string, Items>
        {
            {"Armadura", null},
            {"Espada", null},
            {"Escudo", null}
        };
    }

    // Métodos de jugador...
        /// Metodos
        public void SubirEXP(int Exp){
            if (Exp <= 0) return;

            this.Exp += Exp;
            while (this.Exp >= this.ExpNextLvl)
            {
                this.Exp -= this.ExpNextLvl;
                // Subida de nivel
                Lvl += 1;
                this.Damage += 2;

                Console.WriteLine($"\n -> El jugador: {Nick} subió a nivel {Lvl}.");

                // Desbloqueos de habilidades con el nivel específico
                if (Lvl == 5)
                {
                    Console.WriteLine($"-> Felicidades {Nick}, alcanzaste el nivel 5 y desbloqueaste la habilidad 'Golpe Crítico'.");
                    // Aquí agregar el llamado al método de añadir habilidad.
                }
                if (Lvl == 10)
                {
                    Console.WriteLine($"-> Felicidades {Nick}, alcanzaste el nivel 10 y desbloqueaste la habilidad 'Defensa Activa'.");
                    // Aquí agregar el llamado al método de añadir habilidad.
                }

                // Subida de atributos cada 5 niveles
                if (Lvl % 5 == 0)
                {
                    this.Armor += 2;
                    this.MagicResistance += 2;
                    this.MagicDamage += 1;

                    int temp = this.Mana;
                    this.MaxMana += 10;
                    Mana = (this.MaxMana - temp);
                }

                ExpNextLvl = (3 * ExpNextLvl) / 2;
            }

            Console.WriteLine($"-> El jugador {Nick} tiene [{this.Exp}] pts de experiencia.");
        }

        public void Atacar(Monsters Objetivo){
            if(Objetivo == null){
                return;
            }
            Items Espada;
            //Buscar espada en el diccionario de equipamiento, si no se encuentra se establece como null
            try {
                Espada = Equipamiento["Espada"];
                //Console.WriteLine($"Encontrado: {item.Name}, Valor: {item.Value}");
            } catch (KeyNotFoundException) {
                Espada = null;
                //Console.WriteLine("El objeto no se encuentra en el diccionario.");
            } catch (NullReferenceException){
                Espada = null;    //
            }


            //La del proceso Objetivo.RecibirDmg((Lvl+1)*20, this);
        }
        public void PerderSalud(int dmg){
            if(dmg <= 0){
                Salud -= (dmg/ (Lvl+1));
            }else{
                return;
            }
        }
        public void PerderVida(){
            Vidas -= 1;
            if(Vidas <= 0){
                Console.WriteLine("\n !!! -> "+Nick+" Murio.");
                Estado = false;
            }
        }
    }
}

