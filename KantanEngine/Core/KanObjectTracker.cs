using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.Core
{
    public class KanObjectTracker
    {

        private HashSet<KanGameObject> _trackedObjects = new HashSet<KanGameObject>();

        public IEnumerable<KanGameObject> TrackedGameObjects { get => _trackedObjects.ToList(); }

        public void UpdateTracked() => _trackedObjects.ToList().ForEach(x => x.Update());

        public void Attach(KanGameObject gameObject) => _trackedObjects.Add(gameObject);
        public void Detach(KanGameObject gameObject) => _trackedObjects.Remove(gameObject);
        public void Detach(IEnumerable<KanGameObject> gameObjects) 
            => _trackedObjects.RemoveWhere(x => gameObjects.Contains(x));
        public void Clear() => _trackedObjects.Clear();

    }
}
