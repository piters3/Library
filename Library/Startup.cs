﻿using Owin;

namespace Library {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
