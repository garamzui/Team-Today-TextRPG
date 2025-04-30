namespace TeamTodayTextRPG
{
    public enum ITEM_TYPE
    {
        WEAPON,         //무기
        ARMOR,          //방어구
        POTION      //소모품
    }

    public enum ITEM_CODE
    {
        // 무기
        Weapon_Wooden_Swoard,
        Weapon_Gladius,
        Weapon_Wizard_Wand,
        Weapon_Field_Dagger,
        Weapon_Great_Swoard,
        Weapon_Mithril_Staff,
        Weapon_Unknown_Double_Swoard,
        // 방어구
        Armor_Cloth_Armor,
        Armor_Chain_Mail,
        Armor_Silk_Robe,
        Armor_Leather_Vest,
        Armor_Guardion_Of_The_Earth,
        Armor_Starlight_Robe,
        Armor_Black_Moon,
        // 소모품
        Potion_Health_Small,
        Potion_Health_Middle,
        Potion_Health_Large,
        Potion_Mana_Small,
        Potion_Mana_Middle,
        Potion_Mana_Large,
        Potion_Miracle
    }

    public interface IEquipable
    {
        public void CheckEquip();
        public void Equip();
        public void UnEquip();
    } 
    public interface IUseable
    {
        public void Use();
    }


    public abstract class Item
    {
        public ITEM_TYPE Type { get; set; }
        public ITEM_CODE Code { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int Value { get; set; }
        
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Dodge { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
    }

    // 무기
    public class Weapon_Wooden_Swoard : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }
    public class Weapon_Gladius : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }
    public class Weapon_Wizard_Wand : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }
    public class Weapon_Field_Dagger : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }
    public class Weapon_Great_Swoard : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }
    public class Weapon_Mithril_Staff : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }
    public class Weapon_Unknown_Double_Swoard : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }

    // 방어구
    public class Armor_Cloth_Armor : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }
    public class Armor_Chain_Mail : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }
    public class Armor_Silk_Robe : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }
    public class Armor_Leather_Vest : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }
    public class Armor_Guardion_Of_The_Earth : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }
    public class Armor_Starlight_Robe : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }
    public class Armor_Black_Moon : Item, IEquipable
    {
        public void CheckEquip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void UnEquip()
        {
            throw new NotImplementedException();
        }
    }

    // 소모품
    public class Potion_Health_Small : Item, IUseable
    {
        public void Use()
        {
            throw new NotImplementedException();
        }
    }
    public class Potion_Health_Middle : Item, IUseable
    {
        public void Use()
        {
            throw new NotImplementedException();
        }
    }
    public class Potion_Health_Large : Item, IUseable
    {
        public void Use()
        {
            throw new NotImplementedException();
        }
    }
    public class Potion_Mana_Small : Item, IUseable
    {
        public void Use()
        {
            throw new NotImplementedException();
        }
    }
    public class Potion_Mana_Middle : Item, IUseable
    {
        public void Use()
        {
            throw new NotImplementedException();
        }
    }
    public class Potion_Mana_Large : Item, IUseable
    {
        public void Use()
        {
            throw new NotImplementedException();
        }
    }
    public class Potion_Miracle : Item, IUseable
    {
        public void Use()
        {
            throw new NotImplementedException();
        }
    }
}
