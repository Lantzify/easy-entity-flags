import { UmbApi } from "@umbraco-cms/backoffice/extension-api";
import { EasyEntityFlagsService, EasyEntityFlagModel } from "./api"
import { tryExecute } from "@umbraco-cms/backoffice/resources"
import { UmbControllerHost } from "@umbraco-cms/backoffice/controller-api";

export class EasyEntityFlagsManifests implements UmbApi {

    private _host: UmbControllerHost;
    private _umbExtensionManifest: UmbExtensionManifest[] = [];

    constructor(host: UmbControllerHost) {
        this._host = host;
    }

    destroy(): void {}

    async getEasyEntityFlagModel(): Promise<EasyEntityFlagModel[]> {
        const { data } = await tryExecute(this._host,
            EasyEntityFlagsService.getGetEntityFlags());

        return data ?? new Array<EasyEntityFlagModel>();
    }


    async getEntityFlags()   {

        const flags = await this.getEasyEntityFlagModel()

        flags.forEach(x => {
            const manifest: UmbExtensionManifest = {
                "type": "entitySign",
                "kind": "icon",
                "name": x.name ?? "",
                "alias": x.alias ?? "",
                "weight": x.weight,
                "forEntityTypes": x.forEntityTypes ?? [],
                "forEntityFlags": [x.flagName ?? ""],
                "meta": {
                    "iconName": x.icon,
                    "iconColorAlias": x.iconColorAlias,
                    "label": x.label
                }
            }

            this._umbExtensionManifest?.push(manifest);
        });

        return this._umbExtensionManifest;
    }
}

export default EasyEntityFlagsManifests;