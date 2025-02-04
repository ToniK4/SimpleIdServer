import { createSelector } from '@ngrx/store';
import * as fromApplications from './applications/reducers';
import * as fromDelegateConfigurations from './delegateconfigurations/reducers';
import * as fromGroups from './groups/reducers';
import * as fromHumanTask from './humantasks/reducers';
import * as fromMetadata from './metadata/reducers';
import * as fromProvisioning from './provisioning/reducers';
import * as fromScopes from './scopes/reducers';
import * as fromUsers from './users/reducers';
import * as fromWorkflows from './workflows/reducers';
import * as fromAuthSchemeProviders from './authschemeproviders/reducers';
import * as fromRelyingParty from './relyingparties/reducers';

export interface AppState {
  application: fromApplications.ApplicationState;
  applications: fromApplications.SearchApplicationsState;
  languages: fromMetadata.LanguagesState;
  searchOAuthScopes: fromScopes.SearchOAuthScopesState,
  oauthScopes: fromScopes.OAuthScopesState;
  oauthScope: fromScopes.OAuthScopeState;
  wellKnownConfiguration: fromMetadata.WellKnownConfigurationState,
  users: fromUsers.SearchUsersState,
  user: fromUsers.GetUserState,
  userOpenId: fromUsers.GetUserOpenIdState,
  groups: fromGroups.SearchGroupsState,
  group: fromGroups.GroupState,
  workflowFiles: fromWorkflows.SearchWorkflowFilesState,
  workflowFile: fromWorkflows.WorkflowFileState,
  delegateConfigurations: fromDelegateConfigurations.DelegateConfigurationLstState,
  delegateConfiguration: fromDelegateConfigurations.DelegateConfigurationState,
  humanTaskDefs: fromHumanTask.HumanTaskDefLstState,
  humanTaskDef: fromHumanTask.HumanTaskState,
  workflowInstances: fromWorkflows.SearchWorkflowInstancesState,
  workflowInstance: fromWorkflows.WorkflowInstanceState,
  provisioningHistories: fromProvisioning.ProvisioningHistoriesState,
  provisioningConfigurations: fromProvisioning.ProvisioningConfigurationsState,
  provisioningConfiguration: fromProvisioning.ProvisioningConfigurationState,
  humanTaskInstance: fromHumanTask.HumanTaskInstanceState,
  humanTaskInstances: fromHumanTask.HumanTaskInstancesState,
  authSchemeProvider: fromAuthSchemeProviders.AuthSchemeProviderState,
  authSchemeProviders: fromAuthSchemeProviders.AuthSchemeProvidersState,
  relyingParty: fromRelyingParty.RelyingPartyState,
  relyingParties: fromRelyingParty.SearchRelyingPartiesState
}

export const selectApplication = (state: AppState) => state.application;
export const selectApplications = (state: AppState) => state.applications;
export const selectLanguages = (state: AppState) => state.languages;
export const selectSearchOAuthScopes = (state: AppState) => state.searchOAuthScopes;
export const selectOAuthScopes = (state: AppState) => state.oauthScopes;
export const selectOAuthScope = (state: AppState) => state.oauthScope;
export const selectWellKnownConfiguration = (state: AppState) => state.wellKnownConfiguration;
export const selectUsers = (state: AppState) => state.users;
export const selectUser = (state: AppState) => state.user;
export const selectUserOpenId = (state: AppState) => state.userOpenId;
export const selectGroups = (state: AppState) => state.groups;
export const selectGroup = (state: AppState) => state.group;
export const selectWorkflowFiles = (state: AppState) => state.workflowFiles;
export const selectWorkflowFile = (state: AppState) => state.workflowFile;
export const selectDelegateConfigurations = (state: AppState) => state.delegateConfigurations;
export const selectDelegateConfiguration = (state: AppState) => state.delegateConfiguration;
export const selectHumanTaskDefs = (state: AppState) => state.humanTaskDefs;
export const selectHumanTaskDef = (state: AppState) => state.humanTaskDef;
export const selectWorkflowInstances = (state: AppState) => state.workflowInstances;
export const selectWorkflowInstance = (state: AppState) => state.workflowInstance;
export const selectProvisioningHistories = (state: AppState) => state.provisioningHistories;
export const selectProvisioningConfigurations = (state: AppState) => state.provisioningConfigurations;
export const selectProvisioningConfiguration = (state: AppState) => state.provisioningConfiguration;
export const selectHumanTaskInstance = (state: AppState) => state.humanTaskInstance;
export const selectHumanTaskInstances = (state: AppState) => state.humanTaskInstances;
export const selectAuthSchemeProvider = (state: AppState) => state.authSchemeProvider;
export const selectAuthSchemeProviders = (state: AppState) => state.authSchemeProviders;
export const selectRelyingParty = (state: AppState) => state.relyingParty;
export const selectRelyingParties = (state: AppState) => state.relyingParties;

export const selectAuthSchemeProviderResult = createSelector(
  selectAuthSchemeProvider,
  (state: fromAuthSchemeProviders.AuthSchemeProviderState) => {
    if (!state || state.AuthSchemeProvider === null) {
      return null;
    }

    return state.AuthSchemeProvider;
  }
);

export const selectAuthSchemeProvidersResult = createSelector(
  selectAuthSchemeProviders,
  (state: fromAuthSchemeProviders.AuthSchemeProvidersState) => {
    if (!state || !state.AuthSchemeProviders) {
      return null;
    }

    return state.AuthSchemeProviders;
  }
);

export const selectApplicationResult = createSelector(
  selectApplication,
  (state: fromApplications.ApplicationState) => {
    if (!state || state.Application === null) {
      return null;
    }

    return state.Application;
  }
);

export const selectApplicationsResult = createSelector(
  selectApplications,
  (state: fromApplications.SearchApplicationsState) => {
    if (!state || state.Applications === null) {
      return null;
    }

    return state.Applications;
  }
);

export const selectLanguagesResult = createSelector(
  selectLanguages,
  (state: fromMetadata.LanguagesState) => {
    if (!state || state.Languages === null) {
      return null;
    }

    return state.Languages;
  }
);

export const selectSearchOAuthScopesResult = createSelector(
  selectSearchOAuthScopes,
  (state: fromScopes.SearchOAuthScopesState) => {
    if (!state || state.Scopes === null) {
      return null;
    }

    return state.Scopes;
  }
);

export const selectOAuthScopesResult = createSelector(
    selectOAuthScopes,
  (state: fromScopes.OAuthScopesState) => {
    if (!state || state.Scopes === null) {
      return null;
    }

    return state.Scopes;
  }
);

export const selectOAuthScopeResult = createSelector(
  selectOAuthScope,
  (state: fromScopes.OAuthScopeState) => {
    if (!state || state.Scope === null) {
      return null;
    }

    return state.Scope;
  }
);

export const selectWellKnownConfigurationResult = createSelector(
  selectWellKnownConfiguration,
  (state: fromMetadata.WellKnownConfigurationState) => {
    if (!state || state.Configuration === null) {
      return null;
    }

    return state.Configuration;
  }
);

export const selectUsersResult = createSelector(
  selectUsers,
  (state: fromUsers.SearchUsersState) => {
    if (!state || state.Users === null) {
      return null;
    }

    return state.Users;
  }
);

export const selectUserResult = createSelector(
  selectUser,
  (state: fromUsers.GetUserState) => {
    if (!state || state.User === null) {
      return null;
    }

    return state.User;
  }
);

export const selectUserOpenIdResult = createSelector(
  selectUserOpenId,
  (state: fromUsers.GetUserOpenIdState) => {
    if (!state || state.User === null) {
      return null;
    }

    return state.User;
  }
);

export const selectGroupsResult = createSelector(
  selectGroups,
  (state: fromGroups.SearchGroupsState) => {
    if (!state || !state.Groups) {
      return null;
    }

    return state.Groups;
  }
);

export const selectGroupResult = createSelector(
  selectGroup,
  (state: fromGroups.GroupState) => {
    if (!state || !state.Group) {
      return null;
    }

    return state.Group;
  }
);

export const selectWorkflowFilesResult = createSelector(
  selectWorkflowFiles,
  (state: fromWorkflows.SearchWorkflowFilesState) => {
    if (!state || !state.WorkflowFiles) {
      return null;
    }
    
    return state.WorkflowFiles;
  }
);

export const selectWorkflowFileResult = createSelector(
  selectWorkflowFile,
  (state: fromWorkflows.WorkflowFileState) => {
    if (!state || !state.WorkflowFile) {
      return null;
    }

    return state.WorkflowFile;
  }
);

export const selectDelegateConfigurationSearchResult = createSelector(
  selectDelegateConfigurations,
  (state: fromDelegateConfigurations.DelegateConfigurationLstState) => {
    if (!state || !state.content) {
      return null;
    }

    return state.content;
  }
);

export const selectDelegateConfigurationsResult = createSelector(
  selectDelegateConfigurations,
  (state: fromDelegateConfigurations.DelegateConfigurationLstState) => {
    if (!state || !state.lstIds) {
      return null;
    }

    return state.lstIds;
  }
);

export const selectDelegateConfigurationResult = createSelector(
  selectDelegateConfiguration,
  (state: fromDelegateConfigurations.DelegateConfigurationState) => {
    if (!state || !state.content) {
      return null;
    }

    return state.content;
  }
);

export const selectHumanTaskDefSearchResult = createSelector(
  selectHumanTaskDefs,
  (state: fromHumanTask.HumanTaskDefLstState) => {
    if (!state || !state.content) {
      return null;
    }

    return state.content;
  }
);

export const selectHumanTaskDefsResult = createSelector(
  selectHumanTaskDefs,
  (state: fromHumanTask.HumanTaskDefLstState) => {
    if (!state || !state.lst) {
      return null;
    }

    return state.lst;
  }
);

export const selectHumanTaskDefResult = createSelector(
  selectHumanTaskDef,
  (state: fromHumanTask.HumanTaskState) => {
    if (!state || !state.content) {
      return null;
    }

    return state.content;
  }
);

export const selectWorkflowInstancesResult = createSelector(
  selectWorkflowInstances,
  (state: fromWorkflows.SearchWorkflowInstancesState) => {
    if (!state || !state.WorkflowInstances) {
      return null;
    }

    return state.WorkflowInstances;
  }
);

export const selectWorkflowInstanceResult = createSelector(
  selectWorkflowInstance,
  (state: fromWorkflows.WorkflowInstanceState) => {
    if (!state || !state.WorkflowInstance) {
      return null;
    }

    return state.WorkflowInstance;
  }
);

export const selectProvisioningHistoriesResult = createSelector(
  selectProvisioningHistories,
  (state: fromProvisioning.ProvisioningHistoriesState) => {
    if (!state || !state.ProvisioningHistories) {
      return null;
    }

    return state.ProvisioningHistories;
  }
);

export const selectProvisioningConfigurationsResult = createSelector(
  selectProvisioningConfigurations,
  (state: fromProvisioning.ProvisioningConfigurationsState) => {
    if (!state || !state.ProvisioningConfigurations) {
      return null;
    }

    return state.ProvisioningConfigurations;
  }
);

export const selectProvisioningConfigurationResult = createSelector(
  selectProvisioningConfiguration,
  (state: fromProvisioning.ProvisioningConfigurationState) => {
    if (!state || !state.ProvisioningConfiguration) {
      return null;
    }

    return state.ProvisioningConfiguration;
  }
);

export const selectHumanTaskInstanceResult = createSelector(
  selectHumanTaskInstance,
  (state: fromHumanTask.HumanTaskInstanceState) => {
    if (!state) {
      return null;
    }

    return state;
  }
);

export const selectHumanTaskInstancesResult = createSelector(
  selectHumanTaskInstances,
  (state: fromHumanTask.HumanTaskInstancesState) => {
    if (!state || !state.content) {
      return null;
    }

    return state.content;
  }
);

export const selectRelyingPartyResult = createSelector(
  selectRelyingParty,
  (state: fromRelyingParty.RelyingPartyState) => {
    if (!state || !state.RelyingParty) {
      return null;
    }

    return state.RelyingParty;
  }
);

export const selectRelyingPartiesResult = createSelector(
  selectRelyingParties,
  (state: fromRelyingParty.SearchRelyingPartiesState) => {
    if (!state || !state.RelyingParties) {
      return null;
    }

    return state.RelyingParties;
  }
);

export const appReducer = {
  application: fromApplications.getApplicationReducer,
  applications: fromApplications.getSearchApplicationsReducer,
  languages: fromMetadata.getLanguagesReducer,
  searchOAuthScopes: fromScopes.getSearchOauthScopesReducer,
  oauthScopes: fromScopes.getOAuthScopesReducer,
  oauthScope: fromScopes.getOAuthScopeReducer,
  wellKnownConfiguration: fromMetadata.getWellKnownConfiguration,
  users: fromUsers.getSearchUsersReducer,
  user: fromUsers.getUserReducer,
  userOpenId: fromUsers.getUserOpenIdReducer,
  groups: fromGroups.getSearchGroupsReducer,
  group: fromGroups.getGroupReducer,
  workflowFiles: fromWorkflows.getSearchWorkflowFilesReducer,
  workflowFile: fromWorkflows.getWorkflowFileReducer,
  delegateConfigurations: fromDelegateConfigurations.getDelegateConfigurationLstReducer,
  delegateConfiguration: fromDelegateConfigurations.getDelegateConfigurationReducer,
  humanTaskDefs: fromHumanTask.getHumanTaskLstReducer,
  humanTaskDef: fromHumanTask.getHumanTaskReducer,
  workflowInstances: fromWorkflows.getWorkflowInstancesReducer,
  workflowInstance: fromWorkflows.getWorkflowInstanceReducer,
  provisioningHistories: fromProvisioning.getProvisioningHistoriesReducer,
  provisioningConfigurations: fromProvisioning.getProvisioningConfigurationsReducer,
  provisioningConfiguration: fromProvisioning.getProvisioningConfigurationReducer,
  humanTaskInstance: fromHumanTask.getHumanTaskInstanceReducer,
  humanTaskInstances: fromHumanTask.getHumanTaskInstancesReducer,
  authSchemeProvider: fromAuthSchemeProviders.getAuthSchemeProviderReducer,
  authSchemeProviders: fromAuthSchemeProviders.getAuthSchemeProvidersReducer,
  relyingParty: fromRelyingParty.getRelyingPartyReducer,
  relyingParties: fromRelyingParty.getSearchRelyingPartiesReducer
};
