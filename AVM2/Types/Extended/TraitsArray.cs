using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SwfDec.AVM2.Types.Multinames;
using SwfDec.AVM2.Types.Traits;

namespace SwfDec.AVM2.Types.Extended
{
    public class TraitsArray : IEnumerable<TraitsInfo>, IBrowseable
    {

        public TraitsInfo this[int index]
        {
            get { return _traits[index]; }
            set
            {
               AddTraitAt(value, index);
            }
        }



        public TraitsInfo this[MultinameInfo name] => _traits.FirstOrDefault(t => t.Name == name);

        private readonly List<TraitsInfo> _traits;

        private readonly Dictionary<TraitKind, List<TraitsInfo>> _traitsDictionary;
        public int Count => _traits.Count;

        public TraitsArray(IEnumerable<TraitsInfo> traits)
        {
            _traits = traits.OrderBy(s =>
            {
                if (s.Kind == TraitKind.Const)
                    return -1;
                return (int) s.Kind;
            }).ToList();
            _traitsDictionary = new Dictionary<TraitKind, List<TraitsInfo>>();

            Refresh();
        }

        private void Refresh()
        {
            _traitsDictionary[TraitKind.Slot] = (_traits.Where(n => n.Kind == TraitKind.Slot)).ToList();
            _traitsDictionary[TraitKind.Const] = (_traits.Where(n => n.Kind == TraitKind.Const)).ToList();
            _traitsDictionary[TraitKind.Method] = (_traits.Where(n => n.Kind == TraitKind.Method)).ToList();
            _traitsDictionary[TraitKind.Class] = (_traits.Where(n => n.Kind == TraitKind.Class)).ToList();
            _traitsDictionary[TraitKind.Getter] = (_traits.Where(n => n.Kind == TraitKind.Getter)).ToList();
            _traitsDictionary[TraitKind.Setter] = (_traits.Where(n => n.Kind == TraitKind.Setter)).ToList();
        }

        public TraitsInfo[] GetTraitsByKind(TraitKind traitKind)
        {
            return _traitsDictionary[traitKind].ToArray();
        }

        public TraitsInfo GetByName(String name)
        {
            return _traits.FirstOrDefault(t => t.Name.MKQName.Name == name);
        }

        public IEnumerable<TraitsInfo> GetTraits()
        {
            return _traits.ToArray();
        }

        public void RemoveTraitAt(int index)
        {
            TraitsInfo trait = _traits[index];
            _traitsDictionary[trait.Kind].Remove(trait);
            _traits.Remove(trait);
        }

        public void RemoveTrait(TraitsInfo trait)
        {
            _traitsDictionary[trait.Kind].Remove(trait);
            _traits.Remove(trait);
        }

        public void AddTraitAt(TraitsInfo trait, int index)
        {
            TraitsInfo cTrait = _traits[index];
            _traitsDictionary[cTrait.Kind].Remove(cTrait);

            _traits[index] = trait;
            _traitsDictionary[trait.Kind].Add(trait);
        }

        public void AddTrait(TraitsInfo trait)
        {
            _traitsDictionary[trait.Kind].Add(trait);
            _traits.Add(trait);
        }

        public IEnumerator<TraitsInfo> GetEnumerator()
        {
            return _traits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public object this[String name]
        {
            get { return PropertyHelper.GetProperty(name, this); }
            set { PropertyHelper.SetProeprty(name, this, value); }
        }
    }
}
