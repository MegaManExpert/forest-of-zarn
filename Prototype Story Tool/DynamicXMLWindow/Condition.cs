using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicXMLWindow
{
    public enum ConditionType
    {
        itemLevelReq,
        itemEquip,
        itemHas,
        playerLevel,
        playerAttribute,
        baseDefault,
        useOnce,
        takeItem
    }

    abstract class Condition
    {
        public ConditionType type;
        public bool hasBeenMet = false;
        public string failMessage = "Condition not met";
        public string successMessage = "Condition is met";

        public abstract bool IsThisConditionMet();
    }

    class BaseCondition : Condition
    {
        ConditionType type = ConditionType.baseDefault;
        bool hasBeenMet = false;
        string failMessage = "Condition not met";

        override public bool IsThisConditionMet()
        {
            return false;
        }
    }

    //example concrete condition
    class HasItemCondition : Condition
    {
        ConditionType type = ConditionType.itemHas;
        string itemName;

        public HasItemCondition(string item, string fail, string success, bool met)
        {
            this.itemName = item;
            this.failMessage = fail;
            this.successMessage = success;
            this.hasBeenMet = met;
        }

        override public bool IsThisConditionMet()
        {
            if(Program.player.items.Contains(this.itemName) || hasBeenMet)
            {
                this.hasBeenMet = true;
                return true;
            } else
            {
                return false;
            }
        }
    }

    class HideItemHasCondition : Condition
    {
        ConditionType type = ConditionType.itemHas;
        string itemType;
        int levelReq;

        public HideItemHasCondition(string item, string fail)
        {
            this.itemType = item;
            this.failMessage = fail;
        }

        override public bool IsThisConditionMet()
        {
            if (Program.player.items.Contains(this.itemType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class UseOnceCondition : Condition
    {
        ConditionType type = ConditionType.useOnce;
        bool wasUsed;

        public UseOnceCondition(bool used, string fail)
        {
            this.wasUsed = used;
            this.failMessage = fail;
        }

        override public bool IsThisConditionMet()
        {
            if (wasUsed)
            {
                return false;
            }
            else
            {
                //wasUsed = true;
                return true;
            }
        }
    }
}
