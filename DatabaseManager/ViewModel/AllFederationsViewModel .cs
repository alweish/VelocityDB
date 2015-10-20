﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using DatabaseManager.Model;
using VelocityDb;
using VelocityDb.Session;

namespace DatabaseManager
{
  public class AllFederationsViewModel
  {
    SessionBase m_session;
    readonly ReadOnlyCollection<FederationViewModel> m_federationViews;

    public AllFederationsViewModel()
    {
      SessionBase.BaseDatabasePath = Properties.Settings.Default.BaseDatabasePath;
      m_session = new SessionNoServer(Properties.Settings.Default.DatabaseManagerDirectory);
      m_session.BeginUpdate();
      List<FederationViewModel> federationInfos = new List<FederationViewModel>();
      foreach (FederationInfo info in m_session.AllObjects<FederationInfo>())
        federationInfos.Add(new FederationViewModel(info));
      if (federationInfos.Count() == 0)
      {
        string host = Properties.Settings.Default.DatabaseManagerHost;
        if (host == null || host.Length == 0)
          host = Dns.GetHostName();
        FederationInfo info = new FederationInfo(host,
          Properties.Settings.Default.DatabaseManagerDirectory,
          Properties.Settings.Default.TcpIpPortNumber,
          Properties.Settings.Default.DoWindowsAuthentication,
          null,
          Properties.Settings.Default.UseClientServer,
          "Database Manager");
        m_session.Persist(info);
        m_session.Commit();
        federationInfos.Add(new FederationViewModel(info));
      }
      if (m_session.InTransaction)
        m_session.Commit();
      m_federationViews = new ReadOnlyCollection<FederationViewModel>(federationInfos);
    }

    public ReadOnlyCollection<FederationViewModel> Federations
    {
      get { return m_federationViews; }
    }

    public SessionBase ActiveSession
    {
      get
      {
        return m_session;
      }
    }
  }
}
